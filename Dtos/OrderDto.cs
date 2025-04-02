using AdvancedDb_Order_Inventory.Models;

namespace AdvancedDb_Order_Inventory.Dtos
{
    public class OrderDto
    {
        // Order properties
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int? VoucherId { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ShipmentStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        // OrderItem properties
        public Guid OrderItemId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }

        public Order ExtractOrderInfo()
        {
            return new Order
            {
                Id = this.Id,
                UserId = this.UserId,
                VoucherId = this.VoucherId,
                TotalPrice = this.TotalPrice,
                ShipmentStatus = this.ShipmentStatus,
                PaymentStatus = this.PaymentStatus,
                PaymentMethod = "COD",
                UpdatedDate = this.UpdatedDate ?? DateTime.UtcNow,
                CreatedDate = this.CreatedDate ?? DateTime.UtcNow
            };
        }

        public OrderItem ExtractOrderItemInfo()
        {
            return new OrderItem
            {
                Id = this.OrderItemId,
                OrderId = this.Id,
                ProductId = this.ProductId,
                Quantity = this.Quantity,
                Price = this.Price,
                Subtotal = this.Subtotal
            };
        }
    }
}
