using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

public sealed record CreateProductCommand(ProductRequest Product) : ICommand<Guid>;
