using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.GetProduct.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;

