using Products.DAL.Models;
using Products.Service.Models;
using Products.Service.Utility;

namespace Products.Tests
{
    public class MockData
    {
        public static List<Product> ListOfProducts()
        {
            return
            [
               new()
               {
                   ProductId =100001,
                   ProductName = "Medicine",
                   ProductDescription ="Medical Emergency",
                   Category ="Medicine",
                   Price =278.98M,
                   StockAvailable = 23,
                   CreatedOn = DateTime.Now
               },
               new()
               {
                   ProductId =100002,
                   ProductName = "Car",
                   ProductDescription ="Car",
                   Category ="Vechical",
                   Price =278.98M,
                   StockAvailable = 23,
                   CreatedOn = DateTime.Now
               }
            ];
        }

        public static Product GetProducts()
        {
            return new Product()
            {
                ProductId = 100001,
                ProductName = "Medicine",
                ProductDescription = "Medical Emergency",
                Category = "Medicine",
                Price = 278.98M,
                StockAvailable = 23,
                CreatedOn = DateTime.Now
            };
        }

        public static ProductsDto CreateProductDto()
        {
            return new ProductsDto()
            {
                ProductName = "Medicine",
                ProductDescription = "Medical Emergency",
                Category = "Medicine",
                Price = 278.98M,
                StockAvailable = 23,
                CreatedOn = DateTime.Now
            };
        }
        public static Product CreateProductObject()
        {
            return new Product()
            {
                ProductName = "Medicine",
                ProductDescription = "Medical Emergency",
                Category = "Medicine",
                Price = 278.98M,
                StockAvailable = 23,
                CreatedOn = DateTime.Now
            };
        }

        public static object CreateSuccessObject<T>(T obj)
        {
            return ApiResponse<T>.SuccessResponse(obj);
        }
        public static object CreateFailureObject<T>(string message)
        {
            return ApiResponse<T>.FailureMessage(message);
        }
    }
}
