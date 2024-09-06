using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetProductDetails;

public sealed record GetProductDetailsQuery(Guid ProductId) : IQuery<ProductDetailsResponse>;

