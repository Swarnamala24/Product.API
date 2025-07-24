using Products.DAL.Models;

namespace Products.DAL.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task DeleteProduct(Product product);
        Task<Product?> GetProducts(long id);
        Task<List<Product>> GetProducts();
        Task UpdateProduct(Product product);
    }
}
