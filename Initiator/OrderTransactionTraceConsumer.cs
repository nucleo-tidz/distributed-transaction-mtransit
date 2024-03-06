using Domain;
using MassTransit;

namespace Initiator
{
    public class OrderTransactionTraceConsumer : IConsumer<OrderTransactionTrace>
    {
        public async Task Consume(ConsumeContext<OrderTransactionTrace> context)
        {
            Console.WriteLine($"Order Id: {context.Message.OrderId} failed ");
            await Task.CompletedTask;
        }
    }
}

