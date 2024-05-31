using Microsoft.AspNetCore.Mvc;
using PetShop.DAL;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ContactController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        
        public IActionResult Index()
        {
            //var organics = _dbContext.Organic.ToList();

            //var homeViewModel = new HomeViewModel
            //{
            //    Organics = organics,
            //};

            return View();
        }
    }
}
