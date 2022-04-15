using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    [Index(nameof(StreetAddress), nameof(City), nameof(State), nameof(Zipcode), IsUnique=true)]
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        [Display(Name="Street Address")]
        public string StreetAddress { get; set; }
        [MaxLength(10)]
        public string City { get; set; }
        [StringLength(2)]
        public string State { get; set; }
        [StringLength(5)]
        public string Zipcode { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        public override string ToString()
        {
            return $"{StreetAddress}, {City}, {State}, {Zipcode}";
        }

    }
}
