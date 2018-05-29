using Microsoft.AspNet.Identity.EntityFramework;
using practicingbiteme2.ViewModels;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace practicingbiteme2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Order>()
                        .HasRequired(o => o.Customer)
                        .WithMany(u => u.Orders)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }


        //public System.Data.Entity.DbSet<practicingbiteme2.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}