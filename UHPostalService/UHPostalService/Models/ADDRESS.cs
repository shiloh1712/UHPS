using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string StreetAddress { get; set; }
        [Required]
        [MaxLength(10)]
        public string City { get; set; }
        [Required]
        [MaxLength(2)]
        [MinLength(7)]
        public string State { get; set; }
        [Required]
        [MaxLength(7)]
        [MinLength(7)]
        public string Zipcode { get; set; }


    }
}
