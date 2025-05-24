namespace Contracts;

public record SubmitOrder(Guid OrderId, string ProductId, int Quantity);
public record InventoryReserved(Guid OrderId);
public record InventoryFailed(Guid OrderId, string Reason);
public record PaymentCompleted(Guid OrderId);
public record PaymentFailed(Guid OrderId, string Reason);
public record ShippingStarted(Guid OrderId);
public record OrderCompleted(Guid OrderId);
public record OrderFailed(Guid OrderId, string Reason);
