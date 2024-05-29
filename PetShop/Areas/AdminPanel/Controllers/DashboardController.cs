using Microsoft.AspNetCore.Mvc;

namespace PetShop.Areas.AdminPanel.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
