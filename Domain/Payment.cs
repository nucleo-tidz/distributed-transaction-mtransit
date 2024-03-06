namespace Domain
{
    public class Payment
    {
        public string OrderId { get; set; }
        public double Amount { get; set; }
    }
    public class PaymentLog
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
