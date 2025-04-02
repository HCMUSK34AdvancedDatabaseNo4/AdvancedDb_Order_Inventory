using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdvancedDb_Order_Inventory.Models
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("voucher_id")]
        public int? VoucherId { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("shipment_status")]
        public string ShipmentStatus { get; set; }

        [Column("payment_status")]
        public string PaymentStatus { get; set; }

        [Column("payment_method")]
        public string PaymentMethod { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
