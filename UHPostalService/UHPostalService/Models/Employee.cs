using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public string Email { get; set; }	
		[DataType(DataType.Password)]
		public string Password { get; set; }



		//Navigation Properties: FK
		[Display(Name = "Home Address")]
		public int AddressID { get; set; }
		public Address Address { get; set; }


		//store working at: initially not assigned a store
		[Display(Name = "Work Place")]
		public int ? StoreID { get; set; }
		[ForeignKey("StoreID")]
		//[InverseProperty("Employees")]
		public Store Store { get; set; }

		//Store supervised: might not supervise any store
		//public int ? SupervisedID { get; set; }
		public Store Supervised { get; set; }

	}
}
