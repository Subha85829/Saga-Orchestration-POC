using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Worker.Consumers
{
	public class PaymentCompletedConsumer : IConsumer<PaymentCompleted>
	{
		private readonly ILogger<PaymentCompletedConsumer> _logger;

		public PaymentCompletedConsumer(ILogger<PaymentCompletedConsumer> logger)
		{
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<PaymentCompleted> context)
		{
			var message = context.Message;
			_logger.LogInformation("Shipping started for OrderId: {OrderId}", message.OrderId);

			// Simulate shipping start
			await context.Publish(new ShippingStarted(message.OrderId));
		}
	}
}
