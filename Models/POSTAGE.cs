using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Model
{
    public class POSTAGE
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(6)]
        public string PKG { get; set; }
        [Required]
        public float WEIGHT { get; set; } // need to check that this is <70 but its almost midnight i wanna go to bed

    }
}
