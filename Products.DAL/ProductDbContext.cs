using Microsoft.EntityFrameworkCore;
using Products.DAL.Models;

namespace Products.DAL
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .UseIdentityColumn(seed: 100000, increment: 1);

        }
    }
}
