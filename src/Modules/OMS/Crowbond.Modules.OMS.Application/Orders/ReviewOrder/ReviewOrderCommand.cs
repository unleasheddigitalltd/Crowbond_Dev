using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.ReviewOrder;

public sealed record ReviewOrderCommand(Guid OrderHeaderId): ICommand;
