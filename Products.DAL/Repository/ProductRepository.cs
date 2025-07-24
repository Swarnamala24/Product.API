using Microsoft.EntityFrameworkCore;
using Products.DAL.Models;
using Products.DAL.Repository.Interfaces;

namespace Products.DAL.Repository
{
    public class ProductRepository(ProductDbContext productDbContext) : IProductRepository
    {
        private readonly ProductDbContext _productDbContext = productDbContext;

        public async Task<List<Product>> GetProducts()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProducts(long id)
        {
            return await _productDbContext.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _productDbContext.AddAsync(product);
            
            await _productDbContext.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(Product product)
        {
            _productDbContext.Remove(product);
            await _productDbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _productDbContext.Update(product);

            await _productDbContext.SaveChangesAsync();
        }
    }
}
