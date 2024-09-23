using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.GetCart;

internal sealed class GetCartQueryHandler(
    ICustomerApi customerApi,
    CartService cartService) : IQueryHandler<GetCartQuery, Cart>
{
    public async Task<Result<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        CustomerForOrderResponse? customer = await customerApi.GetByContactIdAsync(request.ContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Cart>(CustomerErrors.ContactNotFound(request.ContactId));
        }

        return await cartService.GetAsync(customer.Id, cancellationToken);
    }
}
