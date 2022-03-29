using System;
using System.ComponentModel.DataAnnotations;

namespace test.Model
{
    public class TrackingRec
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; } //Needs to be from Employee table

        public string TrackNum { get; set; } //12 chars

        [Required]
        public int Store { get; set; } //Needs to be from Store table

        public DateTime TimeIn { get; set; }

        public DateTime TimeOut { get; set; } //Needs to include being Nullable

        public int Destination { get; set; } //Needs to be from Address table

    }
}
