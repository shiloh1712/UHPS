using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Supervisor")]
        public int? SupID { get; set; } 
        public Employee Supervisor { get; set; }
        public int ? AddressID { get; set; }
        public Address Address { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        public override string ToString()
        {

            if (AddressID != null)
                return $"Store {Id}: {Address.ToString()}";
            return "";

        }
    }
}
