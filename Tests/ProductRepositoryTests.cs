using AI_Course_InventorySystem.Models;
using AI_Course_InventorySystem.Repository.Implementations;
using AI_Course_InventorySystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace AI_Course_InventorySystem.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private Mock<DbSet<Product>> _mockSet;
        private Mock<InventoryDbContext> _inventoryDbContext;

        private IProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            var data = new List<Product>
            {
                new Product {Id = 1, Name = "Product1", Description = "Description1", Price = 1.0, Quantity = 1},
                new Product {Id = 2, Name = "Product2", Description = "Description2", Price = 2.0, Quantity = 2},
                new Product {Id = 3, Name = "Product3", Description = "Description3", Price = 3.0, Quantity = 3},
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Product>>();
            _mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _inventoryDbContext = new Mock<InventoryDbContext>();
            _inventoryDbContext.Setup(c => c.Products).Returns(_mockSet.Object);
            _mockSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns((object[] id) => data.FirstOrDefault(x => x.Id == (int)id[0]));

            _repository = new ProductRepository(_inventoryDbContext.Object);
        }

        [Test]
        public void GetProducts_ReturnsAllProducts()
        {
            var result = _repository.GetProducts();

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetProductById_ReturnsCorrectProduct()
        {
            var result = _repository.GetProductById(1);
            Assert.Multiple(() =>
            {
                Assert.That(result?.Id, Is.EqualTo(1));
                Assert.That(result?.Name, Is.EqualTo("Product1"));
            });
        }

        [Test]
        public void InsertProduct_AddsProductToContext()
        {
            var product = new Product { Id = 4, Name = "Product4", Description = "Description4", Price = 4.0, Quantity = 4 };
            _repository.InsertProduct(product);

            _mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void DeleteProduct_RemovesProductFromContext()
        {
            _repository.DeleteProduct(1);

            _mockSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
            _mockSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once);
        }
    }
}