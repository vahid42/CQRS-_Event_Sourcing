using System.ComponentModel.DataAnnotations;

namespace Order_Api.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string? ProductName { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
        }
    }
}
