using Microsoft.Build.Framework;

namespace PetShop.ViewModel
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        public IFormFile? Image { get; set; } = null!;
    }
}
