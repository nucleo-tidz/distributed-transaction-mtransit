using MassTransit;
using Payment;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddActivitiesFromNamespaceContaining<PaymentActivity>();
        
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("create-payment", e =>
                {
                    e.ExecuteActivityHost<PaymentActivity, Domain.Payment>(new Uri("rabbitmq://localhost/create-payment-compensate"));
                });
                cfg.ReceiveEndpoint("create-payment-compensate", e =>
                {
                    e.CompensateActivityHost<PaymentActivity, Domain.PaymentLog>();
                });
            });
        });
        //services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
