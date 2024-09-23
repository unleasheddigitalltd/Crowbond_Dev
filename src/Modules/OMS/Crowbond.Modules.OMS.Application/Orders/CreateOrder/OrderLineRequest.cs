namespace Crowbond.Modules.OMS.Application.Orders.CreateOrder;

public sealed record OrderLineRequest(Guid ProductId, decimal Qty);
