using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.CustomerProducts;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.AddItemToCart;

internal sealed class AddItemToCartCommandHandler(
    ICustomerApi customerApi,
    ICustomerProductApi customerProductApi,
    CartService cartService)
    : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        CustomerForOrderResponse? customer = await customerApi.GetForOrderAsync(request.ContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(request.ContactId));
        }

        CustomerProductResponse? customerProduct = await customerProductApi.GetAsync(customer.Id, request.ProductId, cancellationToken);

        if (customerProduct is null)
        {
            return Result.Failure<Guid>(CustomerProductErrors.NotFound(customer.Id, request.ProductId));
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
            Qty = request.Qty,
            UnitPrice = unitPrice,
        };

        await cartService.AddItemAsync(customer.Id, cartItem, cancellationToken);

        return Result.Success();
    }
}
