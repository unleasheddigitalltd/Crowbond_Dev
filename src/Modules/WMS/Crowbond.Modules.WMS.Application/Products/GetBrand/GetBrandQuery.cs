using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetBrand;

public sealed record GetBrandQuery(Guid BrandId) : IQuery<BrandResponse>;
