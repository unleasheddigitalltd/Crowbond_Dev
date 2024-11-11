using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;
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
        if (request.Customer.CustomPaymentTerms && 
            (request.Customer.DueDateCalculationBasis is null || request.Customer.DueDaysForInvoice is null))
        {
            return Result.Failure<Guid>(CustomerErrors.CustomPaymentTermsParametersHasNoValue);
        }

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
             sequence.GetNumber(),
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
             request.Customer.IsHq,
             request.Customer.ShowPricesInDeliveryDocket,
             request.Customer.ShowPriceInApp,
             request.Customer.ShowLogoInDeliveryDocket);

        customerRepository.Insert(result.Value);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
