using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Display(Name = "Home Address")]
        public int AddressID { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //FK
        public Address Address { get; set; }
    }
}
