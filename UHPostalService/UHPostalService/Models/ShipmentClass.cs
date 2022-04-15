using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class ShipmentClass
    {
        [Key]
        public int Id { get; set; }
        public string Desc { get; set; }
        public float MaxLength { get; set; }
        public float MaxHeight { get; set; }
        public float MaxWidth { get; set; }
        [DataType(DataType.Currency)]
        public float GroundCost { get; set; }
        [DataType(DataType.Currency)]
        public float ExpressCost { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }

    }
}
