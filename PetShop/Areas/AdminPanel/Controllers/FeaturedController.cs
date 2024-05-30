using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using PetShop.DAL;
using PetShop.Model;
using PetShop.ViewModel;

namespace PetShop.Areas.AdminPanel.Controllers
{
    public class FeaturedController : AdminController
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FeaturedController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async  Task<IActionResult> Index()
        {
            var featureds = await _dbContext.FeaturedProducts.ToListAsync();
            return View(featureds);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create(FeaturedViewModel featuredViewModel) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.FeaturedProducts.AnyAsync(x => x.ProductName.ToLower().Equals(featuredViewModel.ProductName.ToLower()));

            if (isExist)
            {
                ModelState.AddModelError("Name", "The name already exists");
                return View();
            }

            if (featuredViewModel.ImageUrl == null)
            {
                ModelState.AddModelError("Image", "Image input can not be empty ");
                return View();
            }

            string file = $"{Guid.NewGuid()}-{featuredViewModel.ImageUrl.FileName}";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "file", "featured", file);
            
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await featuredViewModel.ImageUrl.CopyToAsync(stream);
            }

            FeaturedProducts featured = new()
            {
                ProductName = featuredViewModel.ProductName,
                HrefLink = featuredViewModel.HrefLink,
                ImageUrl = file,
            };


            await _dbContext.FeaturedProducts.AddAsync(featured);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
