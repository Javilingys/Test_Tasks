using Microsoft.EntityFrameworkCore;
using SmartWash.API.Domain.Entities;

namespace SmartWash.API.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<BuyerSale> BuyerSales { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product config
            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .HasMaxLength(100);

            // Buyer Config
            modelBuilder.Entity<Buyer>()
                .Property(x => x.Name)
                .HasMaxLength(75);

            modelBuilder.Entity<Buyer>()
                .Ignore(x => x.SalesIds);

            // Sales Point config
            modelBuilder.Entity<SalesPoint>()
                .Property(x => x.Name)
                .HasMaxLength(125);

            modelBuilder.Entity<SalesPoint>()
                .OwnsMany(x => x.ProvidedProducts);

            // Sales config
            modelBuilder.Entity<Sale>()
                .OwnsMany(x => x.SalesData);
        }
    }
}
