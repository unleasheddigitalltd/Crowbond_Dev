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
            addresstowncity: request.Supplier.AddressTownCity,
            addresscounty: request.Supplier.AddressCounty,
            addresscountry: request.Supplier.AddressCountry,
            addresspostalcode: request.Supplier.AddressPostalCode,
            billingaddressline1: request.Supplier.BillingAddressLine1,
            billingaddressline2: request.Supplier.BillingAddressLine2,
            billingaddresstowncity: request.Supplier.BillingAddressTownCity,
            billingaddresscounty: request.Supplier.BillingAddressCounty,
            billingaddresscountry: request.Supplier.BillingAddressCountry,
            billingaddresspostalcode: request.Supplier.BillingAddressPostalCode,
            suppliercontact: request.Supplier.SupplierContact,
            supplieremail: request.Supplier.SupplierEmail,
            supplierphone: request.Supplier.SupplierPhone,
            suppliernotes: request.Supplier.SupplierNotes,
            paymentterms: request.Supplier.PaymentTerms
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
