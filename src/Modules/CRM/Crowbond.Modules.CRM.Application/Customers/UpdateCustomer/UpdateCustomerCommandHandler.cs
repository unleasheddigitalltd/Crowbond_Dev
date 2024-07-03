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
            businessName: request.Customer.BusinessName,
            driverCode: request.Customer.DriverCode,
            shippingAddressLine1: request.Customer.ShippingAddressLine1,
            shippingAddressLine2: request.Customer.ShippingAddressLine2,
            shippingTownCity: request.Customer.ShippingTownCity,
            shippingCounty: request.Customer.ShippingCounty,
            shippingCountry: request.Customer.ShippingCountry,
            shippingPostalCode: request.Customer.ShippingPostalCode,
            billingAddressLine1: request.Customer.BillingAddressLine1,
            billingAddressLine2: request.Customer.BillingAddressLine2,
            billingTownCity: request.Customer.BillingTownCity,
            billingCounty: request.Customer.BillingCounty,
            billingCountry: request.Customer.BillingCountry,
            billingPostalCode: request.Customer.BillingPostalCode,
            customerContact: request.Customer.CustomerContact,
            customerEmail: request.Customer.CustomerEmail,
            customerPhone: request.Customer.CustomerPhone,
            customerNotes: request.Customer.CustomerNotes,
            priceGroupId: request.Customer.PriceGroupId,
            paymentTerms: request.Customer.PaymentTerms
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
