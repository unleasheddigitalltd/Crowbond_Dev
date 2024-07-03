using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Sequences;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
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

        Result<Customer> result = Customer.Create(
             $"CUS-{sequence.LastNumber + 1}",
             request.Customer.BusinessName,
             request.Customer.DriverCode,
             request.Customer.ShippingAddressLine1,
             request.Customer.ShippingAddressLine2,
             request.Customer.ShippingTownCity,
             request.Customer.ShippingCounty,
             request.Customer.ShippingCountry,
             request.Customer.ShippingPostalCode,
             request.Customer.BillingAddressLine1,
             request.Customer.BillingAddressLine2,
             request.Customer.BillingTownCity,
             request.Customer.BillingCounty,
             request.Customer.BillingCountry,
             request.Customer.BillingPostalCode,
             request.Customer.PriceGroupId,
             request.Customer.PaymentTerms,
             request.Customer.CustomerNotes,
             request.Customer.CustomerEmail,
             request.Customer.CustomerPhone,
             request.Customer.CustomerContact
            );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        customerRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
