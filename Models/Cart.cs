using System.ComponentModel.DataAnnotations;
namespace SalesBoard.Models

{
    public class Cart
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int Item { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; } 
        public int Amount { get; set; }
    }
}
