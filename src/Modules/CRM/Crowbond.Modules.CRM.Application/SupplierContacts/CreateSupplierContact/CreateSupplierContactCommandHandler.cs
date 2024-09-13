using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.CreateSupplierContact;

internal sealed class CreateSupplierContactCommandHandler(
    ISupplierRepository supplierRepository,
    ISupplierContactRepository supplierContactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSupplierContactCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSupplierContactCommand request, CancellationToken cancellationToken)
    {
        Supplier? supplier = await supplierRepository.GetAsync(request.SupplierId, cancellationToken);

        if (supplier == null)
        {
            return Result.Failure<Guid>(SupplierErrors.NotFound(request.SupplierId));
        }

        Result<SupplierContact> result = SupplierContact.Create(
            supplierId: request.SupplierId,
            firstName: request.SupplierContact.FirstName,
            lastName: request.SupplierContact.LastName,
            phoneNumber: request.SupplierContact.PhoneNumber,
            mobile: request.SupplierContact.Mobile,
            email: request.SupplierContact.Email,
            username: request.SupplierContact.Username);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        supplierContactRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
