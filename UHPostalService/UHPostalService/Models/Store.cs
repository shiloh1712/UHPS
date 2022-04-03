using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        [Display(Name = "Supervisor")]
        public int SupID { get; set; } 
        public Employee Supervisor { get; set; }

        //list of regular employees
        public List<Employee> Employees { get; set; }


        [Required]
        public int AddressID { get; set; }
        public Address Address { get; set; }

    }
}
