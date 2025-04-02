using AdvancedDb_Order_Inventory.Dtos;
using AdvancedDb_Order_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedDb_Order_Inventory.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PlaceOrderAsync(Order order, OrderItem orderItem)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                orderItem.OrderId = order.Id;
                await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<(bool Success, List<string> InsufficientProducts)> PlaceOrderTransactionAsync(List<OrderDto> orderDtos)
        {
            // Use one transaction only for the whole process to enable roll back if needed
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var insufficientProducts = new List<string>();

                foreach (var orderDto in orderDtos)
                {
                    // Lock the corresponding product_inventory record with FOR UPDATE
                    var product = await _context.ProductInventories
                        .FromSqlRaw("SELECT * FROM product_inventory WHERE id = {0} FOR UPDATE", orderDto.ProductId)
                        .FirstOrDefaultAsync();

                    if (product == null || product.Stock < orderDto.Quantity)
                    {
                        insufficientProducts.Add(orderDto.ProductId);
                    }
                }

                // If there is any transaction with not enough stock, role back the whole transaction
                if (insufficientProducts.Any())
                {
                    await transaction.RollbackAsync();
                    return (false, insufficientProducts);
                }

                // If all product stock is enough, start saving the order info
                Order order = orderDtos[0].ExtractOrderInfo();
                order.TotalPrice = orderDtos.Sum(p => p.Price * p.Quantity);
                order.Id = Guid.NewGuid();

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                foreach (var orderDto in orderDtos)
                {
                    var orderItem = orderDto.ExtractOrderItemInfo();
                    orderItem.Id = Guid.NewGuid();
                    orderItem.OrderId = order.Id;
                    orderItem.Subtotal = orderItem.Quantity * orderItem.Price;

                    await _context.OrderItems.AddAsync(orderItem);

                    // Update stock info
                    var product = await _context.ProductInventories
                        .FirstOrDefaultAsync(p => p.Id == orderDto.ProductId);

                    product.Stock -= orderDto.Quantity;
                    product.UpdatedDate = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, null);
            }
            catch
            {
                await transaction.RollbackAsync();
                return (false, new List<string>());
            }
        }
    }
}