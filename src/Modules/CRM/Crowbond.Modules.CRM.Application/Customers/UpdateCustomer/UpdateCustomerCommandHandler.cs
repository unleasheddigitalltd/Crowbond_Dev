using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Domain.Reps;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IRepRepository repRepository,
    IPriceTierRepository priceTierRepository,
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

        if (request.Customer.RepId is not null)
        {
            Rep? rep = await repRepository.GetAsync(request.Customer.RepId.Value, cancellationToken);

            if (rep is null)
            {
                return Result.Failure(RepErrors.NotFound(request.Customer.RepId.Value));
            }
        }

        PriceTier? priceTier = await priceTierRepository.GetAsync(request.Customer.PriceTierId, cancellationToken);

        if (priceTier is null)
        {
            return Result.Failure(PriceTierErrors.NotFound(request.Customer.PriceTierId));
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
            showPricesInDeliveryDocket: request.Customer.ShowPricesInDeliveryDocket,
            showPriceInApp: request.Customer.ShowPriceInApp,
            showLogoInDeliveryDocket: request.Customer.ShowLogoInDeliveryDocket,
            customerLogo: request.Customer.CustomerLogo,
            lastModifiedBy: request.UserName,
            lastModifiedDate: dateTimeProvider.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
