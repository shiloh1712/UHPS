using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class ShipmentClass
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string DESCR { get; set; }
        [Required]
        public float LENGTH { get; set; }
        [Required]
        public float HEIGHT { get; set; }
        [Required]
        public float WIDTH { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float GroundCost { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float ExpressCost { get; set; }

    }
}
