using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Utility
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // constructor mechanism

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Assignment> Assignment { get; set; }
    }
}