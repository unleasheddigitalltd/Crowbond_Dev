using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.AddOrderLineToPo;

public sealed record AddOrderLineToPoCommand(Guid OrderLineId, Guid SupplierId) : ICommand;
