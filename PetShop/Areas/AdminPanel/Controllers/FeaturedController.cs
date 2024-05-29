using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.DAL;

namespace PetShop.Areas.AdminPanel.Controllers
{
    public class FeaturedController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public FeaturedController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async  Task<IActionResult> Index()
        {
            var featureds = await _dbContext.FeaturedProducts.ToListAsync();
            return View(featureds);
        }
    }
}
