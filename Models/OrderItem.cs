using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdvancedDb_Order_Inventory.Models
{
    [Table("order_item")]
    public class OrderItem
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Column("product_id")]
        public string ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("subtotal")]
        public decimal Subtotal { get; set; }
    }
}
