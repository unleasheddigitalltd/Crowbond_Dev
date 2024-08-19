using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.CustomerSettings;
using Crowbond.Modules.CRM.Domain.Sequences;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Sequence? sequence = await customerRepository.GetSequenceAsync(cancellationToken);

        if (sequence == null)
        {
            return Result.Failure<Guid>(CustomerErrors.SequenceNotFound());
        }

        sequence.IncreaseSequence();

        Result<Customer> result = Customer.Create(
             accountNumber: $"CUS-{sequence.LastNumber}",
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
             paymentTerms: (PaymentTerm)request.Customer.PaymentTerms,
             invoiceDueDays: request.Customer.InvoiceDueDays,
             deliveryFeeSetting: (DeliveryFeeSetting)request.Customer.DeliveryFeeSetting,
             deliveryMinOrderValue: request.Customer.DeliveryMinOrderValue,
             deliveryCharge: request.Customer.DeliveryCharge,
             noDiscountSpecialItem: request.Customer.NoDiscountSpecialItem,
             noDiscountFixedPrice: request.Customer.NoDiscountFixedPrice,
             detailedInvoice: request.Customer.DetailedInvoice,
             customerNotes: request.Customer.CustomerNotes,
             isHq: request.Customer.IsHq,
             showPricesInDeliveryDocket: request.Customer.ShowPricesInDeliveryDocket,
             showPriceInApp: request.Customer.ShowPriceInApp,
             showLogoInDeliveryDocket: (ShowLogoInDeliveryDocket)request.Customer.ShowLogoInDeliveryDocket,
             customerLogo: request.Customer.CustomerLogo,
             createBy: request.UserId,
             createDate: dateTimeProvider.UtcNow);

        customerRepository.Insert(result.Value);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
