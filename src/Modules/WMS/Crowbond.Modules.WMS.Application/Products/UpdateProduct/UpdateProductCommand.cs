using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(Guid Id, ProductDto Product) : ICommand;

