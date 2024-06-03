using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Products.Application.Products.GetInventoryTypes.Dtos;

namespace Crowbond.Modules.Products.Application.Products.GetInventoryTypes;

public sealed record GetInventoryTypesQuery() : IQuery<IReadOnlyCollection<InventoryTypeResponse>>;
