using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Model
{
    public class SHIPMENT_TYPE
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
        public object UNITCOST { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public object EXPRESSCOST { get; set; }

    }
}
