using System.Windows.Input;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Products.UpdateProduct.Dtos;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(Guid Id, ProductDto Product) : ICommand<ProductDto>;

