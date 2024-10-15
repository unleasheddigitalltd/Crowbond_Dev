using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrderLine;

public sealed record RemoveOrderLineCommand(Guid OrderLineId) : ICommand;
