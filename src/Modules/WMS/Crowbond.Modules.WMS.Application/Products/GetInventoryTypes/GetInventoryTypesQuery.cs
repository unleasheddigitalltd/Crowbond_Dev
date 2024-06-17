using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.GetInventoryTypes.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.GetInventoryTypes;

public sealed record GetInventoryTypesQuery() : IQuery<IReadOnlyCollection<InventoryTypeResponse>>;
