using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetAllCustomersProducts;

public sealed class AllCustomersProductsResponse : PaginatedResponse<CustomerProduct>
{
    public AllCustomersProductsResponse(IReadOnlyCollection<CustomerProduct> customerProducts, IPagination pagination)
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
