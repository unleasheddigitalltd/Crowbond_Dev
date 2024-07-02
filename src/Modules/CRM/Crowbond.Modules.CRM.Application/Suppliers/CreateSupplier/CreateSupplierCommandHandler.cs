using System.Xml.Linq;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Suppliers;

namespace Crowbond.Modules.CRM.Application.Suppliers.CreateSupplier;

internal sealed class CreateSupplierCommandHandler(
        ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSupplierCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {

        Result<Supplier> result = Supplier.Create(
            1,
             request.Supplier.SupplierName,
             request.Supplier.AddressLine1,
             request.Supplier.AddressLine2,
             request.Supplier.AddressTownCity,
             request.Supplier.AddressCounty,
             request.Supplier.AddressCountry,
             request.Supplier.AddressPostalCode,
             request.Supplier.BillingAddressLine1,
             request.Supplier.BillingAddressLine2,
             request.Supplier.BillingAddressTownCity,
             request.Supplier.BillingAddressCounty,
             request.Supplier.BillingAddressCountry,
             request.Supplier.BillingAddressPostalCode,
             request.Supplier.PaymentTerms,
             request.Supplier.SupplierNotes,
             request.Supplier.SupplierEmail,
             request.Supplier.SupplierPhone,
             request.Supplier.SupplierContact
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
