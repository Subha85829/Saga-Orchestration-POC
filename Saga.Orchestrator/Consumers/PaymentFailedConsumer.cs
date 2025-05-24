using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga.Orchestrator.Consumers
{
	public class PaymentFailedConsumer : IConsumer<PaymentFailed>
	{
		private readonly ILogger<PaymentFailedConsumer> _logger;

		public PaymentFailedConsumer(ILogger<PaymentFailedConsumer> logger)
		{
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<PaymentFailed> context)
		{
			_logger.LogWarning("Order {OrderId} failed at payment step: {Reason}", context.Message.OrderId, context.Message.Reason);
			await context.Publish(new OrderFailed(context.Message.OrderId, context.Message.Reason));
		}
	}
}
