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
            var offer_parts = _dbContext.OfferPart.ToList();
            var offers = _dbContext.Offer.ToList();
            var organics = _dbContext.Organic.ToList();
            var hurries = _dbContext.Hurry.ToList();

            var homeViewModel = new HomeViewModel
            {
                FeaturedProducts = featured_products,
                OfferParts = offer_parts,
                Offers = offers,
                Organics = organics,
                Hurries = hurries,
            };

            return View(homeViewModel);
        }
    }
}
