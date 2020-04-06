using Microsoft.EntityFrameworkCore;
namespace StartSportStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)

        {


        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(p => p.Name);
            modelBuilder.Entity<Product>().HasIndex(p => p.Price);
            modelBuilder.Entity<Product>().HasIndex(p => p.RetailPrice);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name);
            modelBuilder.Entity<Category>().HasIndex(c => c.Description);

        }
    }
}
