using Microsoft.AspNetCore.Mvc;
using PetShop.DAL;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public IActionResult Index()
        {

            var featured_products = _dbContext.FeaturedProducts.ToList();

            var homeViewModel = new HomeViewModel
            {
                FeaturedProducts = featured_products,
            };

            return View();
        }
    }
}
