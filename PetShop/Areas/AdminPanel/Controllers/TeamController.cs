using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.DAL;
using PetShop.Model;
using PetShop.ViewModel;

namespace PetShop.Areas.AdminPanel.Controllers
{
    public class TeamController : AdminController
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var teams = await _dbContext.Team.ToListAsync();
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create(TeamViewModel teamViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Team.AnyAsync(x => x.Name.ToLower().Equals(teamViewModel.Name.ToLower()));

            if (isExist)
            {
                ModelState.AddModelError("Name", "The name already exists");
                return View();
            }

            if (teamViewModel.Image == null)
            {
                ModelState.AddModelError("Image", "Image input can not be empty ");
                return View();
            }

            string file = $"{Guid.NewGuid()}-{teamViewModel.Image.FileName}";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "file", "featured", file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await teamViewModel.Image.CopyToAsync(stream);
            }

            Team team = new()
            {
                Name = teamViewModel.Name,
                Description = teamViewModel.Description,
                ImageUrl = file,
            };


            await _dbContext.Team.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var team = await _dbContext.Team.FindAsync(id);

            if (team == null) return NotFound();

            _dbContext.Team.Remove(team);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            var team = await _dbContext.Team.FindAsync(id);

            if (team == null) return NotFound();

            TeamViewModel teamViewModel = new()
            {
                Name = team.Name,
                Description = team.Description,
            };

            return View(teamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, TeamViewModel teamViewModel)
        {
            if (id == null) return NotFound();

            var existTeam = await _dbContext.Team.FindAsync(id);
            if (existTeam is null) return NotFound();

            if (teamViewModel.Image != null)
            {
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "file", "featured", existTeam.ImageUrl);

                if (System.IO.File.Exists(oldPath))
                { System.IO.File.Delete(oldPath); }

                string file = $"{Guid.NewGuid()}-{teamViewModel.Image.FileName}";
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "file", "featured", file);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await teamViewModel.Image.CopyToAsync(stream);
                }

                existTeam.ImageUrl = file;
            }

            existTeam.Name = teamViewModel.Name;
            existTeam.Description = teamViewModel.Description;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
