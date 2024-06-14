using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.GetProductDetails.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.GetProductDetails;

public sealed record GetProductDetailsQuery(Guid ProductId) : IQuery<ProductDetailsResponse>;

