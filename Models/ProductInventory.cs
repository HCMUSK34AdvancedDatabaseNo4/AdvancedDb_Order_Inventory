using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdvancedDb_Order_Inventory.Models
{
    [Table("product_inventory")]
    public class ProductInventory
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("stock")]
        public int Stock { get; set; }
    }
}
