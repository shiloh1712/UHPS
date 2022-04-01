using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Description")]
        public string Desc { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float UnitCost { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}