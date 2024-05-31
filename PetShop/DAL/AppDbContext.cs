using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Model;

namespace PetShop.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<FeaturedProducts> FeaturedProducts { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<OfferPart> OfferPart { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Organic> Organic { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<ShopProduct> ShopProduct { get; set; }

    }
}
