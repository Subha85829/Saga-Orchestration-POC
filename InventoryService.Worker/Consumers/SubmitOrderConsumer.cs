using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Worker.Consumers
{
	public class SubmitOrderConsumer : IConsumer<SubmitOrder>
	{
		private readonly ILogger<SubmitOrderConsumer> _logger;

		public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
		{
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<SubmitOrder> context)
		{
			var message = context.Message;
			_logger.LogInformation("Checking inventory for OrderId: {OrderId}, ProductId: {ProductId}", message.OrderId, message.ProductId);

			// Dummy logic: always reserve inventory unless quantity > 5
			if (message.Quantity <= 5)
			{
				_logger.LogInformation("Inventory available for OrderId: {OrderId}", message.OrderId);
				await context.Publish(new InventoryReserved(message.OrderId));
			}
			else
			{
				_logger.LogWarning("Inventory check failed for OrderId: {OrderId}", message.OrderId);
				await context.Publish(new InventoryFailed(message.OrderId, "Insufficient stock"));
			}
		}
	}
}
