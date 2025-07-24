using Products.DAL.Models;
using Products.Service.Models;

namespace Products.Service.Operations.Interfaces
{
    public interface IProductOperations
    {
        Task<Product?> AddProduct(ProductsDto product);
        Task<bool> DeleteProduct(long id);
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProduct(long id);
        Task<Product?> UpdateProduct(long id, ProductsDto products);
        Task<(bool found, bool success, Product? product)> UpdateStockDecrement(long id, int quantity);
        Task<Product?> UpdateStockIncrement(long id, int quantity);
    }
}
