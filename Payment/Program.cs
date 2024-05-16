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
                cfg.Host("nucleohost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("create-payment", e =>
                {
                    e.ExecuteActivityHost<PaymentActivity, Domain.Payment>(new Uri("rabbitmq://nucleohost/create-payment-compensate"));
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
