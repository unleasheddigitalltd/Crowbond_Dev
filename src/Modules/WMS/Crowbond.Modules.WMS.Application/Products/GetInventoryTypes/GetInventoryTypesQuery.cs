using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetInventoryTypes;

public sealed record GetInventoryTypesQuery() : IQuery<IReadOnlyCollection<InventoryTypeResponse>>;
