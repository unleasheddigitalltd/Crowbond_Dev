using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.Customers.UploadCustomerLogo;

internal sealed class UploadCustomerLogoCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerFileAccess customerFileAccess,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UploadCustomerLogoCommand>
{
    public async Task<Result> Handle(UploadCustomerLogoCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        string logoUrl = await customerFileAccess.SaveLogoAsync(customer.AccountNumber, request.Logo, cancellationToken);

        customer.SetLogo(logoUrl);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
