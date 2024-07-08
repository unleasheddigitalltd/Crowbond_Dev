using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetProductBriefs;

public sealed record GetProductBriefsQuery() : IQuery<IReadOnlyCollection<ProductResponse>>;
