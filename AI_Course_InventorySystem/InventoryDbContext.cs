using AI_Course_InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AI_Course_InventorySystem
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
