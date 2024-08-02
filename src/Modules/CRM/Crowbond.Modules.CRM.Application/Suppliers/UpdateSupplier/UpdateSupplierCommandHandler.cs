using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Application.Suppliers.UpdateSupplier;

internal sealed class UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
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
            suppliername: request.Supplier.SupplierName,
            addressline1: request.Supplier.AddressLine1,
            addressline2: request.Supplier.AddressLine2,
            towncity: request.Supplier.TownCity,
            county: request.Supplier.County,
            country: request.Supplier.Country,
            postalcode: request.Supplier.PostalCode,
            suppliernotes: request.Supplier.SupplierNotes,
            paymentterms: request.Supplier.PaymentTerms
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
