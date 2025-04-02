using AdvancedDb_Order_Inventory.Models;

namespace AdvancedDb_Order_Inventory.Repository
{
    public interface IInventoryRepository
    {
        Task<ProductInventory> GetProductQuantityAsync(string productId);
        Task<bool> UpdateProductQuantityAsync(ProductInventory productInventory);
        Task<List<ProductInventory>> GetAvailableProductsAsync();
    }
}
