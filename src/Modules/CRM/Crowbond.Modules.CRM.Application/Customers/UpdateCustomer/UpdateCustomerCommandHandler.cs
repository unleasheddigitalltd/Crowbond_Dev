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
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerCommand>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request.Customer.CustomPaymentTerms &&
            (request.Customer.DueDateCalculationBasis is null || request.Customer.DueDaysForInvoice is null))
        {
            return Result.Failure(CustomerErrors.CustomPaymentTermsParametersHasNoValue);
        }

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
            request.Customer.BusinessName,
            request.Customer.BillingAddressLine1,
            request.Customer.BillingAddressLine2,
            request.Customer.BillingTownCity,
            request.Customer.BillingCounty,
            request.Customer.BillingCountry,
            request.Customer.BillingPostalCode,
            request.Customer.PriceTierId,
            request.Customer.Discount,
            request.Customer.RepId,
            request.Customer.CustomPaymentTerms,
            request.Customer.DueDateCalculationBasis,
            request.Customer.DueDaysForInvoice,
            request.Customer.DeliveryFeeSetting,
            request.Customer.DeliveryMinOrderValue,
            request.Customer.DeliveryCharge,
            request.Customer.NoDiscountSpecialItem,
            request.Customer.NoDiscountFixedPrice,
            request.Customer.DetailedInvoice,
            request.Customer.CustomerNotes,
            request.Customer.ShowPricesInDeliveryDocket,
            request.Customer.ShowPriceInApp,
            request.Customer.ShowLogoInDeliveryDocket);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
