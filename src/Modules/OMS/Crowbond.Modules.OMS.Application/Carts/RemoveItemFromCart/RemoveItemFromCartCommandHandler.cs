using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.RemoveItemFromCart;

internal sealed class RemoveItemFromCartCommandHandler(
    ICustomerContactApi customerContactApi,
    CartService cartService)
    : ICommandHandler<RemoveItemFromCartCommand>
{
    public async Task<Result> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        CustomerContactResponse customerContact = await customerContactApi.GetAsync(request.ContactId, cancellationToken);

        if (customerContact is null)
        {
            return Result.Failure<Cart>(CustomerErrors.ContactNotFound(request.ContactId));
        }

        await cartService.RemoveItemAsync(customerContact.CustomerId, request.ProductId, cancellationToken);

        return Result.Success();
    }
}
