using AI_Course_InventorySystem.Models;
using AI_Course_InventorySystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AI_Course_InventorySystem.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product? GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(int productId)
        {
            Product product = _context.Products.Find(productId);
            _context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
