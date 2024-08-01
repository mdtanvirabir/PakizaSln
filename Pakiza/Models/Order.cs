namespace Pakiza.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalOrderValue => Products.Sum(p => p.TotalPrice);
    }
}
