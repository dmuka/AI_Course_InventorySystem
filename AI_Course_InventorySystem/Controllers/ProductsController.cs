using AI_Course_InventorySystem.Models;
using AI_Course_InventorySystem.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AI_Course_InventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<ProductDto> PostProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, _mapper.Map<ProductDto>(product));
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public ActionResult PutProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var productToUpdate = _productService.GetProductById(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(productDto, productToUpdate);
            _productService.UpdateProduct(productToUpdate);

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}
