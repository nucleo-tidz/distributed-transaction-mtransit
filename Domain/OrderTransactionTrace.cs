namespace Domain
{
    public record OrderTransactionTrace
    {
        public Guid TrackingNumber { get; init; }
        public DateTime Timestamp { get; init; }
        public string OrderId { get; init; }
        public string ActivityName { get; init; }

    }
}
