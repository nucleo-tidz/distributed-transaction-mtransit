using Invoice;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddActivitiesFromNamespaceContaining<CreateInvoiceActivity>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("nucleohost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("create-invoice", e =>
                {
                    e.ExecuteActivityHost<CreateInvoiceActivity, Domain.Invoice>(new Uri("rabbitmq://nucleohost/create-invoice-compensate"));

                });
                cfg.ReceiveEndpoint("create-invoice-compensate", e =>
                {
                    e.CompensateActivityHost<CreateInvoiceActivity, Domain.InvoiceLog>();
                });
            });
        });
    })
    .Build();

await host.RunAsync();
