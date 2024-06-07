using System.Windows.Input;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Products.Application.Products.CreateProduct.Dtos;

namespace Crowbond.Modules.Products.Application.Products.CreateProduct;

public sealed record CreateProductCommand(ProductRequest Product) : ICommand<ProductResponse>;
