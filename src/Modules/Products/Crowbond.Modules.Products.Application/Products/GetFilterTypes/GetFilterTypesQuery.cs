using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Products.Application.Products.GetFilterTypes.Dtos;

namespace Crowbond.Modules.Products.Application.Products.GetFilterTypes;

public sealed record GetFilterTypesQuery() : IQuery<IReadOnlyCollection<FilterTypeResponse>>;
