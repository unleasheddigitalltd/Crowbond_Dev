using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetBrands;

public sealed record GetBrandsQuery() : IQuery<IReadOnlyCollection<BrandResponse>>;
