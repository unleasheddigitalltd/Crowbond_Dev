using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetFilterTypes;

public sealed record GetFilterTypesQuery() : IQuery<IReadOnlyCollection<FilterTypeResponse>>;
