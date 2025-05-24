using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Worker.Consumers
{
	public class InventoryReservedConsumer : IConsumer<InventoryReserved>
	{
		private readonly ILogger<InventoryReservedConsumer> _logger;

		public InventoryReservedConsumer(ILogger<InventoryReservedConsumer> logger)
		{
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<InventoryReserved> context)
		{
			var message = context.Message;
			_logger.LogInformation("Processing payment for OrderId: {OrderId}", message.OrderId);

			// Simulated logic — fail payment if OrderId ends with an odd digit
			if (message.OrderId.ToString().EndsWith("1") || message.OrderId.ToString().EndsWith("3"))
			{
				_logger.LogWarning("Payment failed for OrderId: {OrderId}", message.OrderId);
				await context.Publish(new PaymentFailed(message.OrderId, "Card declined"));
			}
			else
			{
				_logger.LogInformation("Payment successful for OrderId: {OrderId}", message.OrderId);
				await context.Publish(new PaymentCompleted(message.OrderId));
			}
		}
	}
}
