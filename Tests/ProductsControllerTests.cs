using AI_Course_InventorySystem.Controllers;
using AI_Course_InventorySystem.Models;
using AI_Course_InventorySystem.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AI_Course_InventorySystem.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IProductService> _mockService;
        private Mock<IMapper> _mockMapper;
        private ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IProductService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ProductsController(_mockService.Object, _mockMapper.Object);
        }

        // GET: api/Products
        [Test]
        public void GetProducts_ReturnsAllProducts()
        {
            var products = new List<ProductDto>();
            _mockService.Setup(s => s.GetAllProducts()).Returns(new List<Product>());
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>())).Returns(products);

            var result = _controller.GetProducts();

            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<ProductDto>>>());
        }

        // GET: api/Products/1
        [Test]
        public void GetProduct_ReturnsCorrectProduct()
        {
            var product = new ProductDto { Id = 1 };
            _mockService.Setup(s => s.GetProductById(1)).Returns(new Product { Id = 1 });
            _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(product);

            var result = _controller.GetProduct(1);

            Assert.That(result, Is.InstanceOf<ActionResult<ProductDto>>());
        }

        // POST: api/Products
        [Test]
        public void PostProduct_CreatesProduct()
        {
            var productDto = new ProductDto { Id = 1 };
            var product = new Product { Id = 1 };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);
            _mockService.Setup(s => s.AddProduct(product)).Returns(product);

            var result = _controller.PostProduct(productDto).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result is CreatedAtActionResult, Is.True);

            var createdResult = result as CreatedAtActionResult;

            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult.Value is ProductDto, Is.True);

            var resultValue = createdResult.Value as ProductDto;

            Assert.That(resultValue, Is.EqualTo(productDto));
        }

        // PUT: api/Products/1
        [Test]
        public void PutProduct_UpdatesProduct()
        {
            var productDto = new ProductDto { Id = 1 };
            var product = new Product { Id = 1 };

            _mockService.Setup(s => s.GetProductById(1)).Returns(product);
            _mockMapper.Setup(m => m.Map(productDto, product));

            var result = _controller.PutProduct(1, productDto);

            Assert.That(result, Is.InstanceOf<ActionResult>());
        }

        // DELETE: api/Products/1
        [Test]
        public void DeleteProduct_RemovesProduct()
        {
            var product = new Product { Id = 1 };
            _mockService.Setup(s => s.GetProductById(1)).Returns(product);

            var result = _controller.DeleteProduct(1);

            Assert.That(result, Is.InstanceOf<ActionResult<Product>>());
        }
    }
}