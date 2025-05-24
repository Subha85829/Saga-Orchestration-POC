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

    // Simulated in-memory inventory
    private static readonly Dictionary<string, int> ProductStock = new()
    {
        ["P123"] = 10,
        ["P456"] = 3
    };

    public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SubmitOrder> context)
    {
        var message = context.Message;
        _logger.LogInformation("Checking inventory for OrderId: {OrderId}, ProductId: {ProductId}, Quantity: {Quantity}", 
            message.OrderId, message.ProductId, message.Quantity);

        // Improved inventory check
        if (ProductStock.TryGetValue(message.ProductId, out int available) && available >= message.Quantity)
        {
            _logger.LogInformation("Inventory available for ProductId: {ProductId}. Reserving now.", message.ProductId);
            await context.Publish(new InventoryReserved(message.OrderId));
        }
        else
        {
            _logger.LogWarning("Insufficient stock for ProductId: {ProductId}. Available: {Available}, Requested: {Requested}", 
                message.ProductId, available, message.Quantity);
            await context.Publish(new InventoryFailed(message.OrderId, "Insufficient stock"));
        }
    }
	}
}
