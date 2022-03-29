using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        public int SupervisorId { get; set; } //Needs to be from Employee table

        [Required]
        public int AddressId { get; set; } //Needs to be from Address table

        [Required]
        public char Registration { get; set; } //8 chars

        [Required]
        public string PhoneNum { get; set; }

    }
}
