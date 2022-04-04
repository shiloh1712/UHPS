using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public class TrackingRecord
    {
        [Key]
        public int Id { get; set; }

        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public int TrackNum { get; set; }
        [ForeignKey("TrackNum")]
        public Package Package { get; set; }

        public int? StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; } 

        public int? Destination { get; set; }
        [ForeignKey("Destination")]
        public Address Address { get; set; }
    }
}