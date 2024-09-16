using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.ClearCart;

internal sealed class ClearCartCommandHandler(
    CartService cartService,
    ICustomerContactApi customerContactApi)
    : ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        CustomerContactResponse customerContact = await customerContactApi.GetAsync(request.ContactId, cancellationToken);

        if (customerContact is null)
        {
            return Result.Failure<Guid>(CustomerErrors.ContactNotFound(request.ContactId));
        }

        await cartService.ClearAsync(customerContact.CustomerId, cancellationToken);

        return Result.Success();
    }
}
