using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Model
{
    public class FeaturedProducts
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public string HrefLink { get; set; }
    }
}
