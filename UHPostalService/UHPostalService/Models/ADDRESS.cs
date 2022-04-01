using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    [Index(nameof(StreetAddress), nameof(City), nameof(State), nameof(Zipcode), IsUnique=true)]
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name="Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        [MaxLength(10)]
        public string City { get; set; }
        [Required]
        [StringLength(2)]
        public string State { get; set; }
        [Required]
        [StringLength(7)]
        public string Zipcode { get; set; }


    }
}
