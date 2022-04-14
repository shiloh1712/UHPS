using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string ? PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Home Address")]
        public int? AddressID { get; set; }
        //[ForeignKey("AddressID")]
        public Address? Address { get; set; }
    }
}
