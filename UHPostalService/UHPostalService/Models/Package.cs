using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public enum Status
    {
        [Display(Name ="In Store")]
        InStore,
        [Display(Name = "In Transit")]
        InTransit, 
        [Display(Name = "Out for delivery")]
        OutForDelivery, 
        Delivered, Returned, Lost
    }
    public class Package
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="From")]
        public int ? SenderID { get; set; } 
        [ForeignKey("SenderID")]
        public Customer Sender { get; set; }
        [Display(Name ="To")]
        public int ? ReceiverID { get; set; } 
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
        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }
        [DefaultValue(false)]
        public bool Express { get; set; }
        public int ClassID { get; set; }
        public ShipmentClass Type { get; set; }
        [DataType(DataType.Currency)]
        public float? ShipCost { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        public void Copy(float weight, float height, float width, float depth, bool exp, string desc)
        {
            Description = desc;
            Weight = weight;
            Height = height;
            Width = width;
            Depth = depth;
            Express = exp;
        }
    }
}
