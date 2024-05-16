using Initiator;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                    
                });
                cfg.ReceiveEndpoint("order-events", e =>
                {
                    e.Consumer<OrderTransactionTraceConsumer>();
                });
            });
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
