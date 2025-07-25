using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Products.DAL.Models;
using Products.Service.Controllers;
using Products.Service.Models;
using Products.Service.Operations.Interfaces;
using Products.Service.Utility;

namespace Products.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private  Mock<IProductOperations> _mockProductOperations = default!;
        private ProductsController productsController = default!;

        [TestInitialize]
        public void Setup()
        {
            _mockProductOperations = new Mock<IProductOperations>();
            productsController = new ProductsController(_mockProductOperations.Object);
        }

        [TestMethod]
        public async Task GetAllProducts_ReturnsListOfProducts_ReturnsSuccess()
        {
            List<Product> products = MockData.ListOfProducts();
            var expected = ApiResponse<List<Product>>.SuccessResponse(products);
            //Arrange
            _mockProductOperations.Setup(r => r.GetAllProducts()).ReturnsAsync(products);

            //Act
            var result = await productsController.GetAllProducts() as OkObjectResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetAllProducts_ReturnsEmpty_WhenProductsDoesNotExist()
        {
            List<Product> products = [];
            var expected = ApiResponse<List<Product>>.SuccessResponse(products);
            //Arrange
            _mockProductOperations.Setup(r => r.GetAllProducts()).ReturnsAsync(products);

            //Act
            var result = await productsController.GetAllProducts();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetProducts_ReturnsProduct_WhenProductExists()
        {
            Product products = MockData.GetProducts();
            var expected = ApiResponse<Product>.SuccessResponse(products);
            //Arrange
            _mockProductOperations.Setup(r => r.GetProduct(It.IsAny<long>())).ReturnsAsync(products);

            //Act
            var result = await productsController.GetProduct(100001);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetProducts_ReturnsFailure_WhenProductDoesNotExist()
        {
            Product? products = null;
            object? expected = ApiResponse<Product>.FailureMessage("Product Does Not Exist");
            //Arrange
            _mockProductOperations.Setup(r => r.GetProduct(It.IsAny<long>())).ReturnsAsync(products);

            //Act
            var result = await productsController.GetProduct(100003);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(((NotFoundObjectResult)result));
        }

        [TestMethod]
        public async Task AddProduct_ReturnsProduct_WhenPoductIsAdded()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();

            var expected = ApiResponse<Product>.SuccessResponse(MockData.GetProducts());

            _mockProductOperations.Setup(x => x.AddProduct(It.IsAny<ProductsDto>())).ReturnsAsync(MockData.GetProducts());


            //Act

            var result = await productsController.AddProduct(products);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            Assert.IsNotNull(((CreatedAtActionResult)result));

        }

        [TestMethod]
        public async Task AddProduct_ReturnsFailure_WhenPoductIsAdded()
        {

            //Arrange
            ProductsDto? products = MockData.CreateProductDto();
            Product? response = null;

            var expected = ApiResponse<Product>.FailureMessage("Request body cannnot be null.");
            _mockProductOperations.Setup(x => x.AddProduct(It.IsAny<ProductsDto>())).ReturnsAsync(response);

            //Act

            var result = await productsController.AddProduct(products);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            Assert.IsNotNull(((CreatedAtActionResult)result));

        }

        [TestMethod]
        public async Task UpdateProduct_ReturnsProduct_WhenPoductIsUpdated()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();

            var expected = ApiResponse<Product>.SuccessResponse(MockData.GetProducts());

            _mockProductOperations.Setup(x => x.UpdateProduct(It.IsAny<long>(), It.IsAny<ProductsDto>())).ReturnsAsync(MockData.GetProducts());

            //Act

            var result = await productsController.UpdateProduct(100001, products);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(((OkObjectResult)result));

        }

        public async Task UpdateProduct_ReturnsFailure_WhenPoductDoesNotExist()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();

            var expected = ApiResponse<Product>.FailureMessage("Product Not Found");

            //Act

            var result = await productsController.UpdateProduct(100003, products);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(((OkObjectResult)result));
        }

        [TestMethod]
        public async Task UpdateStockDecement_ReturnsProduct_WhenPoductIsUpdated()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();

            var expected = ApiResponse<Product>.SuccessResponse(MockData.GetProducts());

            _mockProductOperations.Setup(x => x.UpdateStockDecrement(It.IsAny<long>(), It.IsAny<int>())).ReturnsAsync((true, true, MockData.GetProducts()));

            //Act

            var result = await productsController.UpdateStockDecement(100001, 76);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(((OkObjectResult)result));

        }

        [TestMethod]
        public async Task UpdateStockDecement_ReturnsFailure_WhenPoductDoesNotExist()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();
            Product? response = null;
            var expected = ApiResponse<Product>.FailureMessage("Product Not Found Or Stock Is Not Available");
            _mockProductOperations.Setup(x => x.UpdateStockDecrement(It.IsAny<long>(), It.IsAny<int>())).ReturnsAsync((false, false, response));

            //Act

            var result = await productsController.UpdateStockDecement(100003, 23);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(((NotFoundObjectResult)result));
        }

        [TestMethod]
        public async Task UpdateStockIncrement_ReturnsProduct_WhenPoductIsUpdated()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();

            var expected = ApiResponse<Product>.SuccessResponse(MockData.GetProducts());

            _mockProductOperations.Setup(x => x.UpdateStockIncrement(It.IsAny<long>(), It.IsAny<int>())).ReturnsAsync(MockData.GetProducts());

            //Act

            var result = await productsController.UpdateStockIncrement(100001, 76);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(((OkObjectResult)result));

        }

        [TestMethod]
        public async Task UpdateStockIncrement_ReturnsFailure_WhenPoductDoesNotExist()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();
            Product? response = null;
            var expected = ApiResponse<Product>.FailureMessage("Product Not Found");
            _mockProductOperations.Setup(x => x.UpdateStockIncrement(It.IsAny<long>(), It.IsAny<int>())).ReturnsAsync(response);

            //Act

            var result = await productsController.UpdateStockIncrement(100003, 23);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(((NotFoundObjectResult)result));
        }
        [TestMethod]
        public async Task DeleteProduct_ReturnsProduct_WhenPoductIsDeleted()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();

            var expected = ApiResponse<bool>.SuccessResponse(true);

            _mockProductOperations.Setup(x => x.DeleteProduct(It.IsAny<long>())).ReturnsAsync(true);

            //Act

            var result = await productsController.DeleteProduct(100001);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(((OkObjectResult)result));

        }

        [TestMethod]
        public async Task DeleteProduct_ReturnsFailure_WhenPoductDoesNotExist()
        {

            //Arrange
            ProductsDto products = MockData.CreateProductDto();
            var expected = ApiResponse<Product>.FailureMessage("Product Not Found");
            _mockProductOperations.Setup(x => x.DeleteProduct(It.IsAny<long>())).ReturnsAsync(false);

            //Act

            var result = await productsController.DeleteProduct(100003);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(((NotFoundObjectResult)result));
        }

    }
}
