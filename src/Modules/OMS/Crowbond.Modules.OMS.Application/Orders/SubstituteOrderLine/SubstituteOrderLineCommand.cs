using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.SubstituteOrderLine;

public sealed record SubstituteOrderLineCommand(Guid OrderLineId, Guid ProductId, decimal Qty, bool IsBulk) : ICommand<Guid>;
