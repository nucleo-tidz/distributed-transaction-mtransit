using MassTransit;
using Order;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
           
            x.AddActivitiesFromNamespaceContaining<CreateOrderActivity>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("create-order", e =>
                {
                    e.ExecuteActivityHost<CreateOrderActivity,Domain.Order>(new Uri("rabbitmq://localhost/create-order-compensate"));
                   
                });
                cfg.ReceiveEndpoint("create-order-compensate", e =>
                {
                    e.CompensateActivityHost<CreateOrderActivity,Domain.OrderLog>();
                });
            });
        });
    })
    .Build();

await host.RunAsync();
