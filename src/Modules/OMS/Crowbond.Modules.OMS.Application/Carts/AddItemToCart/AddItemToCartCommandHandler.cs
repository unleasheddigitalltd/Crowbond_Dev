using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.CustomerProducts;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Application.Carts.AddItemToCart;

internal sealed class AddItemToCartCommandHandler(
    ICustomerApi customerApi,
    ICustomerProductApi customerProductApi,
    CartService cartService)
    : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        CustomerForOrderResponse? customer = await customerApi.GetByContactIdAsync(request.ContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.ContactNotFound(request.ContactId));
        }

        CustomerProductResponse? customerProduct = await customerProductApi.GetAsync(customer.Id, request.ProductId, cancellationToken);

        if (customerProduct is null)
        {
            return Result.Failure<Guid>(CustomerProductErrors.NotFound(customer.Id, request.ProductId));
        }

        if (!Enum.IsDefined(typeof(TaxRateType), customerProduct.TaxRateType))
        {
            return Result.Failure<Guid>(CustomerProductErrors.InvalidTaxRateType);
        }

        decimal unitPrice = (customer.NoDiscountFixedPrice && customerProduct.IsFixedPrice) ?
            customerProduct.UnitPrice :
            customerProduct.UnitPrice * ((100 - customer.Discount) / 100);

        var cartItem = new CartItem
        {
            ProductId = customerProduct.ProductId,
            ProductName = customerProduct.ProductName,
            ProductSku = customerProduct.ProductSku,
            UnitOfMeasureName = customerProduct.UnitOfMeasureName,
            CategoryId = customerProduct.CategoryId,
            CategoryName = customerProduct.CategoryName,
            BrandId = customerProduct.BrandId,
            BrandName = customerProduct.BrandName,
            ProductGroupId = customerProduct.ProductGroupId,
            ProductGroupName = customerProduct.ProductGroupName,
            Qty = request.Qty,
            UnitPrice = unitPrice,
            TaxRateType = (TaxRateType)customerProduct.TaxRateType
        };

        await cartService.AddItemAsync(customer.Id, cartItem, cancellationToken);

        return Result.Success();
    }
}
