using System.ComponentModel.DataAnnotations;

namespace PetShop.ViewModel
{
    public class FeaturedViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string HrefLink { get; set; }
        public IFormFile? ImageUrl { get; set; } = null;
    }
}
