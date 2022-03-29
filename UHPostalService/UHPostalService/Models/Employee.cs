using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Employee
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
		[Display(Name = "Work Place")]
		public int StoreID { get; set; }


		//Navigation Properties: FK
		public Address Address { get; set; }
		public Store Store { get; set; }
	}
}
