using Contracts;
using MassTransit;

namespace Saga.Orchestrator.Consumers
{
	public class ShippingStartedConsumer : IConsumer<ShippingStarted>
	{
		private readonly ILogger<ShippingStartedConsumer> _logger;

		public ShippingStartedConsumer(ILogger<ShippingStartedConsumer> logger)
		{
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<ShippingStarted> context)
		{
			_logger.LogInformation("Order {OrderId} shipping delivered — marking as completed", context.Message.OrderId);
			await context.Publish(new OrderCompleted(context.Message.OrderId));
		}
	}
}
