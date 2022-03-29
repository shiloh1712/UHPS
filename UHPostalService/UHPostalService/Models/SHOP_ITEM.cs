using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Model
{
    public class SHOP_ITEM
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string DESCR { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public object UnitCost {get; set;}
        [Required]
        public int STOCK { get; set; }

    }
}
