using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerContacts;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactPrimary;

internal sealed class UpdateCustomerContactPrimaryCommandHandler(
    ICustomerContactRepository customerContactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerContactPrimaryCommand>
{
    public async Task<Result> Handle(UpdateCustomerContactPrimaryCommand request, CancellationToken cancellationToken)
    {
        CustomerContact? primaryContact = await customerContactRepository.GetAsync(request.CustomerContactId, cancellationToken);

        if (primaryContact is null)
        {
            return Result.Failure(CustomerContactErrors.NotFound(request.CustomerContactId));
        }

        IEnumerable<CustomerContact> contacts = await customerContactRepository.GetByCustomerIdAsync(primaryContact.CustomerId, cancellationToken);

        foreach (CustomerContact contact in contacts)
        {
            Result result = contact.ChangePrimary(contact.Id == primaryContact.Id);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
