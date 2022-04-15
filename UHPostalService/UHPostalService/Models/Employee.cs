using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
	public enum Role
    {
		Admin, Supervisor, Employee
    }
    public class Employee
    {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }	
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name = "Home Address")]
		public int AddressID { get; set; }
		//[ForeignKey("AddressID")]
		public Address Address { get; set; }
		//store working at: initially not assigned a store
		[Display(Name = "Work Place")]
		public int ? StoreID { get; set; }
		public Store Store { get; set; }
		public Role Role { get; set; }
		[DefaultValue(false)]
		public bool Deleted { get; set; }

	}
}
