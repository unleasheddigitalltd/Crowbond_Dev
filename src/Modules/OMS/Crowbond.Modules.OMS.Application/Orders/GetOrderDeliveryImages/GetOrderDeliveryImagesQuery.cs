using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderDeliveryImages;

public sealed record GetOrderDeliveryImagesQuery(Guid OrderHeaderId): IQuery<IReadOnlyCollection<DeliveryImageResponse>>;
