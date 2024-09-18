using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.CustomerSettings;
using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Domain.Reps;
using Crowbond.Modules.CRM.Domain.Sequences;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IRepRepository repRepository,
    IPriceTierRepository priceTierRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request.Customer.RepId is not null)
        {
            Rep? rep = await repRepository.GetAsync(request.Customer.RepId.Value, cancellationToken);

            if (rep is null)
            {
                return Result.Failure<Guid>(RepErrors.NotFound(request.Customer.RepId.Value));
            }
        }

        PriceTier? priceTier = await priceTierRepository.GetAsync(request.Customer.PriceTierId, cancellationToken);

        if (priceTier is null)
        {
            return Result.Failure<Guid>(PriceTierErrors.NotFound(request.Customer.PriceTierId));
        }

        Sequence? sequence = await customerRepository.GetSequenceAsync(cancellationToken);

        if (sequence == null)
        {
            return Result.Failure<Guid>(CustomerErrors.SequenceNotFound());
        }

        Result<Customer> result = Customer.Create(
             accountNumber: sequence.GetNumber(),
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
             showLogoInDeliveryDocket: (ShowLogoInDeliveryDocket)request.Customer.ShowLogoInDeliveryDocket);

        customerRepository.Insert(result.Value);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
