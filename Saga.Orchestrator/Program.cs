using MassTransit;
using Saga.Orchestrator.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<InventoryFailedConsumer>();
	x.AddConsumer<PaymentFailedConsumer>();
	x.AddConsumer<ShippingStartedConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host("rabbitmq", "/", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});

		cfg.ConfigureEndpoints(context);
	});
});

var host = builder.Build();
host.Run();
