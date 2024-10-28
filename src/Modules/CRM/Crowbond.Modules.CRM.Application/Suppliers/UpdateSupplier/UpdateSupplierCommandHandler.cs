using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;

internal sealed class UpdateSupplierCommandHandler(
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSupplierCommand>
{
    public async Task<Result> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        Supplier? supplier = await supplierRepository.GetAsync(request.Id, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure(SupplierErrors.NotFound(request.Id));
        }

        supplier.Update(
            request.Supplier.SupplierName,
            request.Supplier.AddressLine1,
            request.Supplier.AddressLine2,
            request.Supplier.TownCity,
            request.Supplier.County,
            request.Supplier.Country,
            request.Supplier.PostalCode,
            request.Supplier.SupplierNotes);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
