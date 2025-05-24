using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga.Orchestrator.Consumers
{
	public class InventoryFailedConsumer : IConsumer<InventoryFailed>
	{
		private readonly ILogger<InventoryFailedConsumer> _logger;

		public InventoryFailedConsumer(ILogger<InventoryFailedConsumer> logger)
		{
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<InventoryFailed> context)
		{
			_logger.LogWarning("Order {OrderId} failed at inventory step: {Reason}", context.Message.OrderId, context.Message.Reason);
			await context.Publish(new OrderFailed(context.Message.OrderId, context.Message.Reason));
		}
	}
}
