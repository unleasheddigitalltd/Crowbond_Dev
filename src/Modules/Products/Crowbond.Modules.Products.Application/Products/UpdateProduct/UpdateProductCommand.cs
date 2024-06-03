using System.Windows.Input;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Products.Application.Products.UpdateProduct.Dtos;

namespace Crowbond.Modules.Products.Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(Guid Id, ProductDto Product) : ICommand<ProductDto>;

