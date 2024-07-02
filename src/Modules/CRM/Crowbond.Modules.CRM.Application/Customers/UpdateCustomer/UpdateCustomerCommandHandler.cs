using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerCommand>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.Id));
        }

        customer.Update(
            businessname: request.Customer.BusinessName,
            drivercode: request.Customer.DriverCode,
            shippingaddressline1: request.Customer.ShippingAddressLine1,
            shippingaddressline2: request.Customer.ShippingAddressLine2,
            shippingtowncity: request.Customer.ShippingTownCity,
            shippingcounty: request.Customer.ShippingCounty,
            shippingcountry: request.Customer.ShippingCountry,
            shippingpostalcode: request.Customer.ShippingPostalCode,
            billingaddressline1: request.Customer.BillingAddressLine1,
            billingaddressline2: request.Customer.BillingAddressLine2,
            billingtowncity: request.Customer.BillingTownCity,
            billingcounty: request.Customer.BillingCounty,
            billingcountry: request.Customer.BillingCountry,
            billingpostalcode: request.Customer.BillingPostalCode,
            customercontact: request.Customer.CustomerContact,
            customeremail: request.Customer.CustomerEmail,
            customerphone: request.Customer.CustomerPhone,
            customernotes: request.Customer.CustomerNotes,
            pricegroupid: request.Customer.PriceGroupId,
            paymentterms: request.Customer.PaymentTerms
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
