using Microsoft.AspNetCore.Mvc;
using PetShop.DAL;
using PetShop.ViewModel;

namespace PetShop.Controllers
{
    public class GalleryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public GalleryController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public IActionResult Index()
        {

            var gallery = _dbContext.Gallery.ToList();

            var galleryViewModel = new GalleryViewModel
            {
                Galleries = gallery,
            };

            return View();
        }
    }
}
