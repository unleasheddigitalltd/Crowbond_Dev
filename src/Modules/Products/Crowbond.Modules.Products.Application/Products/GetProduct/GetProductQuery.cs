using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Products.Application.Products.GetProduct.Dtos;

namespace Crowbond.Modules.Products.Application.Products.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;

