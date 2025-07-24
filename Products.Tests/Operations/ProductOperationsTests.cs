using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Products.DAL.Models;
using Products.DAL.Repository.Interfaces;
using Products.Service.Operations;

namespace Products.Tests.Operations
{
    [TestClass]
    public class ProductOperationsTests
    {
        private  Mock<IProductRepository> _mockProductRepository =default!;
        private ProductsOperations _productsOperations =default!;

        [TestInitialize]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productsOperations = new ProductsOperations(_mockProductRepository.Object);
        }

        [TestMethod]
        public async Task GetAllProducts_ReturnsListOfProducts_WhenProductExist()
        {
            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts()).ReturnsAsync(MockData.ListOfProducts());

            //Act
            var result = await _productsOperations.GetAllProducts();

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAllProducts_ReturnsNoProducts_WhenProductsDoesNotExist()
        {
            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts()).ReturnsAsync([]);

            //Act
            var result = await _productsOperations.GetAllProducts();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public async Task GetProduct_ReturnsProduct_WhenProductExist()
        {
            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(MockData.GetProducts());

            //Act
            var result = await _productsOperations.GetProduct(100001);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Product));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetProduct_ReturnsNull_WhenProductDoesNotExist()
        {
            Product? product = null;
            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);

            //Act
            var result = await _productsOperations.GetProduct(100004);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnTrue_WhenProductIsDeleted()
        {
            Product? product = MockData.GetProducts();
            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.DeleteProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.DeleteProduct(100001);

            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnFalse_WhenProductDoesNotExist()
        {
            Product? product = null;
            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.DeleteProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.DeleteProduct(100003);

            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public async Task UpdateProduct_ReturnsProduct_WhenProductIsUpdated()
        {
            Product? product = MockData.GetProducts();

            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.UpdateProduct(100001, MockData.CreateProductDto());

            //Assert
            Assert.IsInstanceOfType(result, typeof(Product));
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task UpdateProduct_ReturnsNull_WhenProductDoesNotExist()
        {
            Product? product = null;

            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.UpdateProduct(100003, MockData.CreateProductDto());

            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task UpdateStockIncrement_ReturnsProduct_WhenStockIsIncremented()
        {
            Product? product = MockData.GetProducts();

            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.UpdateStockIncrement(100003, 23);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Product));
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task UpdateStockIncrement_ReturnsNull_WhenProductDoesNotExist()
        {
            Product? product = null;

            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.UpdateStockIncrement(100003, 23);

            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task UpdateStockDecrement_ReturnsNull_WhenProductDoesNotExist()
        {
            Product? product = null;

            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.UpdateStockDecrement(100003, 23);

            //Assert
            Assert.IsNull(result.product);
            Assert.IsFalse(result.found);
            Assert.IsFalse(result.success);

        }


        [TestMethod]
        public async Task UpdateStockDecrement_ReturnsProduct_WhenStockIsDecremented()
        {
            Product? product = MockData.GetProducts();

            //Arrange
            _mockProductRepository.Setup(x => x.GetProducts(It.IsAny<long>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.UpdateProduct(It.IsAny<Product>()));

            //Act
            var result = await _productsOperations.UpdateStockDecrement(100001, 23);

            //Assert
            Assert.IsNotNull(result.product);
            Assert.IsTrue(result.found);
            Assert.IsTrue(result.success);

        }


        [TestMethod]
        public async Task AddProduct_ReturnsProduct_WhenProductIsCreated()
        {
            Product? product = MockData.CreateProductObject();

            //Arrange
            _mockProductRepository.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);

            //Act
            var result = await _productsOperations.AddProduct(MockData.CreateProductDto());

            //Assert
            Assert.IsInstanceOfType(result, typeof(Product));
            Assert.IsNotNull(result);

        }

    }
}
