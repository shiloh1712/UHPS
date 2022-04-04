using System.ComponentModel.DataAnnotations;

namespace UHPostalService.Models
{
    public class Sale
    {
        [Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        [DataType(DataType.Currency)]
        public float? Total { get; set; }
    }
}
