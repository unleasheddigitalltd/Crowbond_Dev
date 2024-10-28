using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Sequences;
using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Application.Suppliers.CreateSupplier;

internal sealed class CreateSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSupplierCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        Sequence? sequence = await supplierRepository.GetSequenceAsync(cancellationToken);

        if (sequence == null)
        {
            return Result.Failure<Guid>(SupplierErrors.SequenceNotFound());
        }

        Result<Supplier> result = Supplier.Create(
             sequence.GetNumber(),
             request.Supplier.SupplierName,
             request.Supplier.AddressLine1,
             request.Supplier.AddressLine2,
             request.Supplier.TownCity,
             request.Supplier.County,
             request.Supplier.Country,
             request.Supplier.PostalCode,
             request.Supplier.SupplierNotes);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        supplierRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
