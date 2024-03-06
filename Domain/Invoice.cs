namespace Domain
{
    public class Invoice
    {
        public string OrderId { get; set; }
        public string InvoiceId { get; set; }
    }
    public class InvoiceLog : ActivityLog
    {
    }
}
