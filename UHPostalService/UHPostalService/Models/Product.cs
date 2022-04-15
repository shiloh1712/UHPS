using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Description")]
        public string Desc { get; set; }
        [DataType(DataType.Currency)]
        [Range(0,1000)]
        public float UnitCost { get; set; }
        [Range(0, 1000)]
        public int Stock { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}