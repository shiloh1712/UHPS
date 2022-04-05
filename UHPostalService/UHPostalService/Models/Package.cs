using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public enum Status
    {
        InStore, InTransit, OutForDelivery, Delivered, Returned, Lost
    }
    public class Package
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="From")]
        public int SenderID { get; set; } 
        [ForeignKey("SenderID")]
        public Customer Sender { get; set; }
        [Display(Name ="To")]
        public int ReceiverID { get; set; } 
        [ForeignKey("ReceiverID")]
        public Customer Receiver { get; set; }
        [Display(Name ="Destination")]
        public int AddressID { get; set; } 
        [ForeignKey("AddressID")]
        public Address Destination { get; set; }

        public string? Description { get; set; }
        [DefaultValue(Status.InStore)]
        public Status Status { get; set; }
        public float Weight { get; set; }
        [DefaultValue(false)]
        public bool Express { get; set; }
        [DataType(DataType.Currency)]
        public float? ShipCost { get; set; }
    }
}
