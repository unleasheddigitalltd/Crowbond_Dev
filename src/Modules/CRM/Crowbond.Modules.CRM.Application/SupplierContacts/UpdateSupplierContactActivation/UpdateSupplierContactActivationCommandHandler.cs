using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.SupplierContacts;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactActivation;

internal sealed class UpdateSupplierContactActivationCommandHandler(
    ISupplierContactRepository supplierContactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSupplierContactActivationCommand>
{
    public async Task<Result> Handle(UpdateSupplierContactActivationCommand request, CancellationToken cancellationToken)
    {
        SupplierContact? contact = await supplierContactRepository.GetAsync(request.SupplierContactId, cancellationToken);

        if (contact == null)
        {
            return Result.Failure(SupplierContactErrors.NotFound(request.SupplierContactId));
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
