using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.Customers.RemoveCustomerLogo;

internal sealed class RemoveCustomerLogoCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerFileAccess customerFileAccess,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveCustomerLogoCommand>
{
    public async Task<Result> Handle(RemoveCustomerLogoCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerContactErrors.NotFound(request.CustomerId));
        }

        await customerFileAccess.DeleteLogoAsync(customer.AccountNumber, cancellationToken);
        customer.RemoveLogo();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
