using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Sale
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PURCHASE_DATE { get; set; }

    }
}
