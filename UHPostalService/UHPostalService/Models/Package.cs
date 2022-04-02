using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public enum Status
    {
        InStore, InTransit, OutForDelivery, Delivered, Returned
    }
    public class Package
    {
        [Key]
        public string TrackingNum { get; set; }
        [MinLength(6)]
        [MaxLength(6)]

        [Required]
        public int ComingFrom { get; set; } 
		public Customer Customer { get; set; }

        [Required]
        public int RecipientId { get; set; } 
        public Customer Customer { get; set; }

        [Required]
        public int GoingTo { get; set; }
        public Address Address { get; set; }

        [Required]
        public string Description { get; set; } 
        [MaxLength(60)]

        public string Status { get; set; } 
        public DateTime TimeDelivered { get; set; }
    }
}
