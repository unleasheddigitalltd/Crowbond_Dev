using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.CreateProduct.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

public sealed record CreateProductCommand(ProductRequest Product) : ICommand<ProductResponse>;
