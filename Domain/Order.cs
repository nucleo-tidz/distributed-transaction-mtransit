namespace Domain
{
    public class Order
    {
        public string OrderId { get; set; }
        public IEnumerable<string> Items { get; set; }
    }
    public class OrderLog
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
