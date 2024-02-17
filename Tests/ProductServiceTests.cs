using AI_Course_InventorySystem.Models;
using AI_Course_InventorySystem.Repository.Interfaces;
using AI_Course_InventorySystem.Services.Implementations;
using Moq;
using NUnit.Framework;

namespace AI_Course_InventorySystem.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepository;
        private ProductService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepository.Object);
        }

        [Test]
        public void GetAllProducts_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>()
        {
            new Product {Id = 1, Name = "Product1", Description = "Description1", Price = 1.0, Quantity = 1},
            new Product {Id = 2, Name = "Product2", Description = "Description2", Price = 2.0, Quantity = 2},
            new Product {Id = 3, Name = "Product3", Description = "Description3", Price = 3.0, Quantity = 3}
        }.AsEnumerable();

            _mockRepository.Setup(r => r.GetProducts()).Returns(products);

            // Act
            var result = _service.GetAllProducts();

            // Assert
            Assert.That(result, Is.EquivalentTo(products));
        }

        [Test]
        public void GetProductById_ReturnsCorrectProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 1.0, Quantity = 1 };
            _mockRepository.Setup(r => r.GetProductById(1)).Returns(product);

            // Act
            var result = _service.GetProductById(1);

            // Assert
            Assert.That(result, Is.EqualTo(product));
        }

        [Test]
        public void AddProduct_InsertsAndSavesProduct()
        {
            // Arrange
            var product = new Product { Id = 4, Name = "Product4", Description = "Description4", Price = 4.0, Quantity = 4 };

            // Act
            _service.AddProduct(product);

            // Assert
            _mockRepository.Verify(r => r.InsertProduct(product), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void DeleteProduct_DeletesAndSavesProduct()
        {
            // Arrange
            var product = new Product { Id = 1 };

            _mockRepository.Setup(r => r.GetProductById(1)).Returns(product);

            // Act
            _service.DeleteProduct(1);

            // Assert
            _mockRepository.Verify(r => r.DeleteProduct(1), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void UpdateProduct_UpdatesAndSavesProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Description = "UpdatedDescription", Price = 2.0, Quantity = 1 };

            _mockRepository.Setup(r => r.GetProductById(1)).Returns(product);

            // Act
            var updatedProduct = _service.UpdateProduct(product);

            // Assert
            _mockRepository.Verify(r => r.UpdateProduct(product), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
            Assert.That(updatedProduct, Is.EqualTo(product));
        }
    }
}