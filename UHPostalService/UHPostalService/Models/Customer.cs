using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
