using Products.DAL.Models;
using Products.DAL.Repository.Interfaces;
using Products.Service.Models;
using Products.Service.Operations.Interfaces;

namespace Products.Service.Operations
{
    public class ProductsOperations(IProductRepository productRepository) : IProductOperations
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Product?> AddProduct(ProductsDto product)
        {
            try
            {
                Product item = new()
                {
                    ProductName = product.ProductName ?? string.Empty,
                    ProductDescription = product.ProductDescription ?? string.Empty,
                    Price = product.Price!.Value,
                    Category = product.Category,
                    StockAvailable = product.StockAvailable!.Value,
                    CreatedOn = DateTime.Now
                };

               return await _productRepository.AddProduct(item);

            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product?> GetProduct(long id)
        {
            var result = await _productRepository.GetProducts(id);
            return result;
        }

        public async Task<Product?> UpdateProduct(long id, ProductsDto products)
        {
            try
            {
                Product? product = await _productRepository.GetProducts(id);

                if (product != null)
                {
                    product.ProductName = string.IsNullOrEmpty(products.ProductName)?product.ProductName : products.ProductName;
                    product.ProductDescription = string.IsNullOrEmpty(products.ProductDescription) ? product.ProductDescription : products.ProductDescription;
                    product.StockAvailable = products.StockAvailable ?? product.StockAvailable;
                    product.Price = products.Price??product.Price;
                    product.Category = string.IsNullOrEmpty(products.Category) ? product.Category : products.Category;
                    product.UpdatedOn = DateTime.Now;

                    await _productRepository.UpdateProduct(product);

                }
                return product;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Product?> UpdateProduct(long id, int quantity)
        {

            Product? product = await _productRepository.GetProducts(id);

            if (product == null)
            {
                return null;

            }
            product.StockAvailable += quantity;

            await _productRepository.UpdateProduct(product);
            return product;
        }


        public async Task<Product?> UpdateStockIncrement(long id, int quantity)
        {

            Product? product = await _productRepository.GetProducts(id);

            if (product == null)
                return null;

            product.StockAvailable += quantity;

            await _productRepository.UpdateProduct(product);
            return product;
        }

        public async Task<(bool found, bool success, Product? product)> UpdateStockDecrement(long id, int quantity)
        {
            var product = await _productRepository.GetProducts(id);
            if (product == null)
                return (false, false, null);
           
            if (product?.StockAvailable < quantity) 
                return (false, false, null);

            product!.StockAvailable -= quantity;
            product.UpdatedOn = DateTime.Now;
            await _productRepository.UpdateProduct(product);

            return (true, true, product);
        }
        
        public async Task<bool> DeleteProduct(long id)
        {
            Product? product = await _productRepository.GetProducts(id);
            
            if(product == null) return false;   

            await _productRepository.DeleteProduct(product);
            return true;
        }





    }
}
