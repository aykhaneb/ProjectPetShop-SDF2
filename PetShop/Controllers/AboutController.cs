using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.DAL;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AboutController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public IActionResult Index()
        {

            var teams = _dbContext.Team.ToList();

            var aboutViewModel = new AboutViewModel
            {
                Teams = teams,
            };

            return View(aboutViewModel);
        }
    }
}
