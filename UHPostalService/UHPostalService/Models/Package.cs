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
        public int SenderID { get; set; } //Needs to be from Customer table
        [ForeignKey("SenderID")]
        public Customer Sender { get; set; }

        public int ReceiverID { get; set; } //Needs to be from Customer table
        [ForeignKey("ReceiverID")]
        public Customer Receiver { get; set; }

        public int AddrToID { get; set; } //Needs to be from Address table
        [ForeignKey("AddrToID")]
        public Address ToAddress { get; set; }

        public string Description { get; set; }
        [DefaultValue(Status.InStore)]
        public Status Status { get; set; }
        public float Weight { get; set; }
        [DefaultValue(false)]
        public bool Express { get; set; }
        [DataType(DataType.Currency)]
        public float ShipCost { get; set; }
    }
}
