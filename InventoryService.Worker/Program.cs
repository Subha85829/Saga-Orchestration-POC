using InventoryService.Worker.Consumers;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<SubmitOrderConsumer>();

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
