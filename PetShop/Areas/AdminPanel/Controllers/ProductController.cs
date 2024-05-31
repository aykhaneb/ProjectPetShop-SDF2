using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using PetShop.DAL;
using PetShop.Model;
using PetShop.ViewModel;

namespace PetShop.Areas.AdminPanel.Controllers
{
    public class ProductController : AdminController
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var shopPrdoucts = await _dbContext.ShopProduct.ToListAsync();
            return View(shopPrdoucts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.ShopProduct.AnyAsync(x => x.Name.ToLower().Equals(productViewModel.Name.ToLower()));

            if (isExist)
            {
                ModelState.AddModelError("Name", "The name already exists");
                return View();
            }

            if (productViewModel.Image == null)
            {
                ModelState.AddModelError("Image", "Image input can not be empty ");
                return View();
            }

            string file = $"{Guid.NewGuid()}-{productViewModel.Image.FileName}";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "file", "featured", file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await productViewModel.Image.CopyToAsync(stream);
            }

            ShopProduct shopProduct = new()
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                ImageUrl = file,
            };


            await _dbContext.ShopProduct.AddAsync(shopProduct);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var shopProduct = await _dbContext.ShopProduct.FindAsync(id);

            if (shopProduct == null) return NotFound();

            _dbContext.ShopProduct.Remove(shopProduct);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            var shopProduct = await _dbContext.ShopProduct.FindAsync(id);

            if (shopProduct == null) return NotFound();

            ProductViewModel productViewModel = new()
            {
                Name = shopProduct.Name,
                Price = shopProduct.Price,
            };

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ProductViewModel productViewModel)
        {
            if (id == null) return NotFound();

            var existProduct = await _dbContext.ShopProduct.FindAsync(id);
            if (existProduct is null) return NotFound();

            if (productViewModel.Image != null)
            {
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "file", "featured", existProduct.ImageUrl);

                if (System.IO.File.Exists(oldPath))
                { System.IO.File.Delete(oldPath); }

                string file = $"{Guid.NewGuid()}-{productViewModel.Image.FileName}";
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "file", "featured", file);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await productViewModel.Image.CopyToAsync(stream);
                }

                existProduct.ImageUrl = file;
            }

            existProduct.Name = productViewModel.Name;
            existProduct.Price = productViewModel.Price;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
