using AI_Course_InventorySystem.Models;
using AI_Course_InventorySystem.Repository.Interfaces;
using AI_Course_InventorySystem.Services.Interfaces;

namespace AI_Course_InventorySystem.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public Product AddProduct(Product product)
        {
            _productRepository.InsertProduct(product);
            _productRepository.Save();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
            _productRepository.Save();
            return product;
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            _productRepository.Save();
        }
    }
}
