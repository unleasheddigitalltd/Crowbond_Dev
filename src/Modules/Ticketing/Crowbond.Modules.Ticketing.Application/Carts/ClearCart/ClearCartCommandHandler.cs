using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Ticketing.Application.Carts;
using Crowbond.Modules.Ticketing.Domain.Customers;

namespace Crowbond.Modules.Ticketing.Application.Carts.ClearCart;

internal sealed class ClearCartCommandHandler(ICustomerRepository customerRepository, CartService cartService)
    : ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        await cartService.ClearAsync(customer.Id, cancellationToken);

        return Result.Success();
    }
}
