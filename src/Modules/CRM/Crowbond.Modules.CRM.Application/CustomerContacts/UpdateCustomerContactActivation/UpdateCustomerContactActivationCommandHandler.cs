using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerContacts;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactActivation;

internal sealed class UpdateCustomerContactActivationCommandHandler(
    ICustomerContactRepository customerContactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerContactActivationCommand>
{
    public async Task<Result> Handle(UpdateCustomerContactActivationCommand request, CancellationToken cancellationToken)
    {
        CustomerContact? contact = await customerContactRepository.GetAsync(request.CustomerContactId, cancellationToken);

        if (contact == null)
        {
            return Result.Failure(CustomerContactErrors.NotFound(request.CustomerContactId));
        }

        Result result = request.IsActive ? 
            contact.Activate() : 
            contact.Deactivate();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
