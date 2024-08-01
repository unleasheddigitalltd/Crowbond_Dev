using System.Xml.Linq;
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

        sequence.IncreaseSequence();
        Result<Supplier> result = Supplier.Create(
             accountNumber: $"SUP-{sequence.LastNumber}",
             supplierName: request.Supplier.SupplierName,
             addressLine1: request.Supplier.AddressLine1,
             addressLine2: request.Supplier.AddressLine2,
             townCity: request.Supplier.TownCity,
             county: request.Supplier.County,
             country: request.Supplier.Country,
             postalCode: request.Supplier.PostalCode,
             paymentTerms: request.Supplier.PaymentTerms,
             supplierNotes: request.Supplier.SupplierNotes
            );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        supplierRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
