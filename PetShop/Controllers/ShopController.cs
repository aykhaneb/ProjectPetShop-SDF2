using Microsoft.AspNetCore.Mvc;
using PetShop.DAL;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var shopProduct = _dbContext.ShopProduct.ToList();

            var shopViewModel = new ShopViewModel
            {
                ShopProducts = shopProduct,
            };

            return View(shopViewModel);
        }
    }
}
