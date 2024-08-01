using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerCommand>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        customer.Update(
            businessName: request.Customer.BusinessName,
            billingAddressLine1: request.Customer.BillingAddressLine1,
            billingAddressLine2: request.Customer.BillingAddressLine2,
            billingTownCity: request.Customer.BillingTownCity,
            billingCounty: request.Customer.BillingCounty,
            billingCountry: request.Customer.BillingCountry,
            billingPostalCode: request.Customer.BillingPostalCode,
            priceTierId: request.Customer.PriceTierId,
            discount: request.Customer.Discount,
            repId: request.Customer.RepId,
            customPaymentTerm: request.Customer.CustomPaymentTerm,
            paymentTerms: request.Customer.PaymentTerms,
            invoiceDueDays: request.Customer.InvoiceDueDays,
            deliveryFeeSetting: request.Customer.DeliveryFeeSetting,
            deliveryMinOrderValue: request.Customer.DeliveryMinOrderValue,
            deliveryCharge: request.Customer.DeliveryCharge,
            noDiscountSpecialItem: request.Customer.NoDiscountSpecialItem,
            noDiscountFixedPrice: request.Customer.NoDiscountFixedPrice,
            detailedInvoice: request.Customer.DetailedInvoice,
            customerNotes: request.Customer.CustomerNotes,
            isHq: request.Customer.IsHq,
            showPricesInDeliveryDocket: request.Customer.ShowPricesInDeliveryDocket,
            showPriceInApp: request.Customer.ShowPriceInApp,
            showLogoInDeliveryDocket: request.Customer.ShowLogoInDeliveryDocket,
            customerLogo: request.Customer.CustomerLogo,
            isActive: request.Customer.IsActive,
            lastModifiedBy: request.UserName,
            lastModifiedDate: dateTimeProvider.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
