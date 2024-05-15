using Microsoft.AspNetCore.Mvc;
using PetShop.DAL;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public IActionResult Index()
        {

            var blog = _dbContext.Blog.ToList();

            var blogViewModel = new BlogViewModel
            {
                Blogs = blog,
            };

            return View();
        }
    }
}
