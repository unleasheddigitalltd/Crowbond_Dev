﻿using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.SupplierContacts;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContact;

internal sealed class UpdateSupplierContactCommandHandler(
    ISupplierContactRepository supplierContactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSupplierContactCommand>
{
    public async Task<Result> Handle(UpdateSupplierContactCommand request, CancellationToken cancellationToken)
    {
        SupplierContact? supplierContact = await supplierContactRepository.GetAsync(request.SupplierContactId, cancellationToken);

        if (supplierContact == null)
        {
            return Result.Failure(SupplierContactErrors.NotFound(request.SupplierContactId));       
        }

        supplierContact.Update(
            firstName: request.SupplierContact.FirstName,
            lastName: request.SupplierContact.LastName,
            phoneNumber: request.SupplierContact.PhoneNumber,
            mobile: request.SupplierContact.Mobile);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
