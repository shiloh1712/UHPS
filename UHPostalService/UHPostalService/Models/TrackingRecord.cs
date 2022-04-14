using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public class TrackingRecord
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int TrackNum { get; set; }
        public Package Package { get; set; }
        public int? StoreId { get; set; }
        public Store? Store { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; } 
        public int? Destination { get; set; }
        public Address? Address { get; set; }
    }
}