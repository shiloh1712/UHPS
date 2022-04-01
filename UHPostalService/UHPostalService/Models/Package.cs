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
        public string TrackingNum { get; set; } //6 characters

        [Required]
        public int ComingFrom { get; set; } //Needs to be from Customer table

        [Required]
        public int RecipientId { get; set; } //Needs to be from Customer table

        [Required]
        public int GoingTo { get; set; } //Needs to be from Address table

        [Required]
        public string Description { get; set; } //Max 60 characters

        public string Status { get; set; } //Needs to be a range of options from:
                                           //in store, in transit, out for delivery, delivered, lost, returned

        public DateTime TimeDelivered { get; set; }
    }
}
