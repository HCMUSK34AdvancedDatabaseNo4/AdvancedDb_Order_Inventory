using AdvancedDb_Order_Inventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AdvancedDb_Order_Inventory.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().ToTable("order");
            modelBuilder.Entity<OrderItem>().ToTable("order_item");
            modelBuilder.Entity<ProductInventory>().ToTable("product_inventory");
        }
    }
}
