using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Postage
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(6)]
        public string PackageID { get; set; }
        [Required]
        public float WEIGHT { get; set; } // need to check that this is <70 but its almost midnight i wanna go to bed

    }
}
