using AdvancedDb_Order_Inventory.Dtos;
using AdvancedDb_Order_Inventory.Models;

namespace AdvancedDb_Order_Inventory.Repository
{
    public interface IOrderRepository
    {
        Task<bool> PlaceOrderAsync(Order order, OrderItem orderItem);
        Task<(bool Success, List<string> InsufficientProducts)> PlaceOrderTransactionAsync(List<OrderDto> orderDtos);
    }
}