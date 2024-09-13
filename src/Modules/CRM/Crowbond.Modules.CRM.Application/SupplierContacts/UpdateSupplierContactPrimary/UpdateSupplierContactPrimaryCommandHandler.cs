using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.SupplierContacts;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactPrimary;

internal sealed class UpdateSupplierContactPrimaryCommandHandler(
    ISupplierContactRepository supplierContactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSupplierContactPrimaryCommand>
{
    public async Task<Result> Handle(UpdateSupplierContactPrimaryCommand request, CancellationToken cancellationToken)
    {
        SupplierContact? primaryContact = await supplierContactRepository.GetAsync(request.SupplierContactId, cancellationToken);

        if (primaryContact == null)
        {
            return Result.Failure(SupplierContactErrors.NotFound(request.SupplierContactId));
        }

        IEnumerable<SupplierContact> contacts = await supplierContactRepository.GetBySupplierIdAsync(primaryContact.SupplierId, cancellationToken);

        foreach (SupplierContact contact in contacts)
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
