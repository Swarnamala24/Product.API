using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.DAL;
using Products.DAL.Models;
using Products.DAL.Repository;

namespace Products.Tests.Repository
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private ProductDbContext _context = default!;
        private ProductRepository _repository = default!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ProductDbContext(options);
            _repository = new ProductRepository(_context);
        }

        [TestMethod]
        public async Task GetAllProducts_ReturnsListOfProducts_WhenProductsExist()
        {
            //Arrange
            _context.Products.AddRange(MockData.ListOfProducts());

            //Act
            var result = await _repository.GetProducts();
            //Arrange
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAllProducts_ReturnsEmptyList_WhenProductsDoesNotExist()
        {
            //Act
            var result = await _repository.GetProducts();
            //Arrange
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetProducts_ReturnsProduct_WhenProductsExist()
        {
            //Arrange
            _context.Products.AddRange(MockData.ListOfProducts());

            //Act
            var result = await _repository.GetProducts(100001);
            //Arrange
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetProducts_ReturnsNull_WhenProductsDoesNotExist()
        {
            //Arrange
            _context.Products.AddRange(MockData.ListOfProducts());

            //Act
            var result = await _repository.GetProducts(100005);
            //Arrange
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddProduct_ReturnsProduct_WhenProductsIsCreated()
        {
            //Arrange
            await _context.Products.AddAsync(MockData.CreateProductObject());

            //Act
            var result = await _repository.AddProduct(MockData.CreateProductObject());
            //Arrange
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateProduct_ReturnsVoid_WhenProductIsUpdated()
        {
            //Arrange
            await _context.Products.AddAsync(MockData.CreateProductObject());

            Product product = MockData.CreateProductObject();
            product.StockAvailable = 554;
            _context.Products.Update(product);

            //Act
            await _repository.UpdateProduct(MockData.CreateProductObject());

            var result = await _repository.GetProducts(product.ProductId);
            //Arrange
            Assert.AreEqual(554, result?.StockAvailable);
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnsVoid_WhenProductIsDelete()
        {
            //Arrange
            await _context.Products.AddAsync(MockData.CreateProductObject());

            Product product = MockData.CreateProductObject();

            //Act
            await _repository.UpdateProduct(MockData.CreateProductObject());

            var result = await _repository.GetProducts(product.ProductId);
            //Arrange
            Assert.IsNull(result);
        }

    }
}
