using System.ComponentModel.DataAnnotations;

namespace SalesBoard.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } 
        public int Quantity { get; set; }
        public string Seller { get; set; }
    }
}
