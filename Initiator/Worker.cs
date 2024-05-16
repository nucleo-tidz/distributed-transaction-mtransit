using Domain;
using MassTransit;
using MassTransit.Courier.Contracts;
using MassTransit.Courier.Messages;
using domain = Domain;
namespace Initiator
{
   
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Guid trackingNumber = NewId.NextGuid();
            var builder = new RoutingSlipBuilder(trackingNumber);
            builder.AddActivity("CreateOrderActivity", new Uri("rabbitmq://nucleohost/create-order"), new domain.Order { OrderId = "OD123", Items = new List<string> { "Toy" } });
            builder.AddActivity("PaymentActivity", new Uri("rabbitmq://nucleohost/create-payment"), new domain.Payment { OrderId = "OD123", Amount = 10 });
            builder.AddActivity("InvoiceActivity", new Uri("rabbitmq://nucleohost/create-invoice"), new domain.Invoice { OrderId = "OD123" });
            await builder.AddSubscription(new Uri("rabbitmq://nucleohost/order-events"),
                                           RoutingSlipEvents.Faulted,
                                           x => x.Send<OrderTransactionTrace>(new
                                           {
                                               OrderId = "OD123",
                                           
                                           }));
            RoutingSlip routingSlip = builder.Build();
            await _bus.Execute(routingSlip);

        }
    } 
}
