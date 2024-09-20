using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.RemoveItemFromCart;

internal sealed class RemoveItemFromCartCommandHandler(
    ICustomerApi customerApi,
    CartService cartService)
    : ICommandHandler<RemoveItemFromCartCommand>
{
    public async Task<Result> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        CustomerForOrderResponse? customer = await customerApi.GetForOrderAsync(request.ContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Cart>(CustomerErrors.NotFound(request.ContactId));
        }

        await cartService.RemoveItemAsync(customer.Id, request.ProductId, request.Qty, cancellationToken);

        return Result.Success();
    }
}
