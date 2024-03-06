namespace Domain
{
    public class Order
    {
        public string OrderId { get; set; }
        public IEnumerable<string> Items { get; set; }
    }
    public class OrderLog : ActivityLog
    {
    }
}
