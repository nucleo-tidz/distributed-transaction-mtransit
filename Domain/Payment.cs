namespace Domain
{
    public class Payment
    {
        public string OrderId { get; set; }
        public double Amount { get; set; }
    }
    public class PaymentLog : ActivityLog
    {
        public string PaymentId { get; set; }
    }
}
