using Microsoft.Build.Framework;

namespace PetShop.ViewModel
{
    public class TeamViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
