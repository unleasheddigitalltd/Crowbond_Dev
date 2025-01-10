using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetExpiredCustomerProducts;

public sealed class CustomerProductsResponse : PaginatedResponse<CustomerProduct>
{
    public CustomerProductsResponse(IReadOnlyCollection<CustomerProduct> customerProducts, IPagination pagination)
        : base(customerProducts, pagination)
    { }
}

public sealed record CustomerProduct(
    Guid Id,
    Guid CustomerId,
    string BusinessNema,
    Guid ProductId,
    string ProductName,
    decimal BasePurchasePrice,
    decimal SalePrice,
    decimal? FixedPrice,
    decimal? FixedDiscount,
    DateOnly EffectiveDate,
    DateOnly? ExpiryDate);
