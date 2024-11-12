namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductBlacklist;

public sealed record ProductResponse(
    Guid Id,
    Guid CustomerId,
    Guid ProductId,
    string ProductName, 
    string ProductSku,
    string UnitOfMeasureName,
    Guid CategoryId,
    string CategoryName,
    Guid BrandId,
    string BrandName,
    Guid ProductGroupId,
    string ProductGroupName,
    string? Comments);
