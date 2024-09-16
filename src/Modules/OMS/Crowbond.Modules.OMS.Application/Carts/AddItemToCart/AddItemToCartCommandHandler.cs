using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.CustomerProducts;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.AddItemToCart;

internal sealed class AddItemToCartCommandHandler(
    ICustomerContactApi customerApi,
    ICustomerProductApi customerProductApi,
    CartService cartService)
    : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        CustomerContactResponse customerContact = await customerApi.GetAsync(request.ContactId, cancellationToken);

        if (customerContact is null)
        {
            return Result.Failure<Guid>(CustomerErrors.ContactNotFound(request.ContactId));
        }

        CustomerProductResponse? customerProduct = await customerProductApi.GetAsync(customerContact.CustomerId, request.ProductId, cancellationToken);

        if (customerProduct is null)
        {
            return Result.Failure<Guid>(CustomerProductErrors.NotFound(customerContact.CustomerId, request.ProductId));
        }

        var cartItem = new CartItem
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Price = customerProduct.UnitPrice,
        };

        await cartService.AddItemAsync(customerContact.CustomerId, cartItem, cancellationToken);

        return Result.Success();
    }
}
