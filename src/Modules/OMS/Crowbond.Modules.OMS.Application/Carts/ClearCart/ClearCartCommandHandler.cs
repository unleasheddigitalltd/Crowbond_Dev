using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;

namespace Crowbond.Modules.OMS.Application.Carts.ClearCart;

internal sealed class ClearCartCommandHandler(
    CartService cartService,
    ICustomerApi customerApi)
    : ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        CustomerForOrderResponse customer = await customerApi.GetByContactIdAsync(request.ContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.ContactNotFound(request.ContactId));
        }

        await cartService.ClearAsync(customer.Id, cancellationToken);

        return Result.Success();
    }
}
