using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Sale
    {
        [Key]
        public int ID { get; set; }
        public int ?ProductID { get; set; }
        public Product Product { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int ? BuyerID { get; set; }
        public Customer? Buyer { get; set; }
        [DataType(DataType.Currency)]
        public float? Total { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}
