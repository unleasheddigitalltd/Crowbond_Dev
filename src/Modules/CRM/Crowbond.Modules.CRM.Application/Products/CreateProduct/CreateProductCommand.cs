using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Domain.Products;

namespace Crowbond.Modules.CRM.Application.Products.CreateProduct;

public sealed record CreateProductCommand(
    Guid ProductId,
    string Name,
    string Sku,
    string FilterTypeName,
    string UnitOfMeasureName,
    string InventoryTypeName,
    Guid CategoryId,
    string CategoryName,
    TaxRateType TaxRateType,
    bool IsActive)
    : ICommand;
