using PetShop.Model;

namespace PetShop.ViewModel
{
    public class HomeViewModel
    {
        public List<FeaturedProducts>? FeaturedProducts { get; set; }
        public List<OfferPart>? OfferParts { get; set; }
        public List<Offer>? Offers { get; set; }
        public List<Organic>? Organics { get; set; }
        public List<Hurry>? Hurries { get; set; }
    }
}
