using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrderDeliveryImage;

public sealed record RemoveOrderDeliveryImageCommand(Guid OrderId, string ImageName) : ICommand;
