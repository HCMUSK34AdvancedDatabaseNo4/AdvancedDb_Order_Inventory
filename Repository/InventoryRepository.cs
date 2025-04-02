using AdvancedDb_Order_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedDb_Order_Inventory.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductInventory> GetProductQuantityAsync(string productId)
        {
            return await _context.ProductInventories
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<bool> UpdateProductQuantityAsync(ProductInventory productInventory)
        {
            var existingProduct = await _context.ProductInventories
                .FirstOrDefaultAsync(p => p.Id == productInventory.Id);

            if (existingProduct == null)
                return false;

            existingProduct.Stock = productInventory.Stock;
            existingProduct.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductInventory>> GetAvailableProductsAsync()
        {
            return await _context.ProductInventories
                .Where(p => p.Stock > 0)
                .ToListAsync();
        }
    }
}
