namespace Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;

public sealed record DispatchLineRequest(Guid ProductId, decimal QuantityReceived);
