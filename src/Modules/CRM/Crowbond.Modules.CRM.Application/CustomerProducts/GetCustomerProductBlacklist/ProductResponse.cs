namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductBlacklist;

public sealed record ProductResponse(
    Guid Id,
    Guid CustomerId,
    Guid ProductId,
    string ProductName, 
    string ProductSku,
    string UnitOfMeasureName,
    string CategoryName,
    string BrandName,
    string ProductGroupName,
    string? Comments);
