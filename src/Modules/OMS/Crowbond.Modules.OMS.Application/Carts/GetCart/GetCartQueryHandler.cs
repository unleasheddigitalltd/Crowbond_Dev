using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.GetCart;

internal sealed class GetCartQueryHandler(
    ICustomerContactApi customerContactApi,
    CartService cartService) : IQueryHandler<GetCartQuery, Cart>
{
    public async Task<Result<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        CustomerContactResponse customerContact = await customerContactApi.GetAsync(request.ContactId, cancellationToken);

        if (customerContact is null)
        {
            return Result.Failure<Cart>(CustomerErrors.ContactNotFound(request.ContactId));
        }

        return await cartService.GetAsync(customerContact.CustomerId, cancellationToken);
    }
}
