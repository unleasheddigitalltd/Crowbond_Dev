﻿using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplierActivation;

internal sealed class UpdateSupplierActivationCommandHandler(
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSupplierActivationCommand>
{
    public async Task<Result> Handle(UpdateSupplierActivationCommand request, CancellationToken cancellationToken)
    {
        Supplier? supplier = await supplierRepository.GetAsync(request.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure(SupplierErrors.NotFound(request.SupplierId));
        }

        Result result = request.IsActive ? supplier.Activate() : supplier.Deactivate();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
