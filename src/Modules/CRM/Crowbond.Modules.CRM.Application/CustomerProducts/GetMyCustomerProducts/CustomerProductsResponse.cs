﻿using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetMyCustomerProducts;

public sealed class CustomerProductsResponse : PaginatedResponse<CustomerProduct>
{

    public CustomerProductsResponse(IReadOnlyCollection<CustomerProduct> customerProducts, IPagination pagination)
        : base(customerProducts, pagination)
    { }
}

public sealed record CustomerProduct(
    Guid Id,
    Guid CustomerId,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    string CategoryName,
    string BrandName,
    string ProductGroupName,
    decimal UnitPrice,
    decimal FinalUnitPrice);
