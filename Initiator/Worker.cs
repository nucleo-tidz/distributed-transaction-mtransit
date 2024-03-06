using MassTransit;
using MassTransit.Courier.Contracts;
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
         
            var builder = new RoutingSlipBuilder(NewId.NextGuid());
            builder.AddActivity("CreateOrderActivity", new Uri("rabbitmq://localhost/create-order"), new domain.Order { OrderId = "OD123", Items = new List<string> { "Toy" } });
            builder.AddActivity("PaymentActivity", new Uri("rabbitmq://localhost/create-payment"), new domain.Payment { OrderId = "OD123", Amount = 10 });
            builder.AddActivity("InvoiceActivity", new Uri("rabbitmq://localhost/create-invoice"), new domain.Invoice { OrderId = "OD123"});
            RoutingSlip routingSlip = builder.Build();
      
            await _bus.Execute(routingSlip);
      
        }
    }
}
