using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Sequences;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerContactRepository customerContactRepository,
    ICustomerOutletRepository customerShippingAddressRepository,
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
             $"CUS-{sequence.LastNumber}",
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
             request.Customer.CustomPaymentTerm,             
             request.Customer.PaymentTerms,
             request.Customer.InvoiceDueDays,
             request.Customer.DeliveryFeeSetting,
             request.Customer.DeliveryMinOrderValue,
             request.Customer.DeliveryCharge,
             request.Customer.NoDiscountSpecialItem,
             request.Customer.NoDiscountFixedPrice,
             request.Customer.ShowPricesInDeliveryDocket,
             request.Customer.ShowPriceInApp,
             request.Customer.DetailedInvoice,
             request.Customer.CustomerNotes,
             request.Customer.IsHq,
             request.UserId,
             dateTimeProvider.UtcNow);




        customerRepository.Insert(result.Value);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        IEnumerable<CustomerContact> customerContacts = request.Customer.CustomerContacts
            .Select(c => CustomerContact.Create(
                result.Value.Id,
                c.FirstName,
                c.LastName,
                c.Username,
                c.PhoneNumber,
                c.Mobile,
                c.Email,
                c.Primary,
                c.ReceiveInvoice,
                c.ReceiveOrder,
                c.ReceivePriceList,
                request.UserId,
                dateTimeProvider.UtcNow));

        customerContactRepository.InsertRange(customerContacts);

        IEnumerable<CustomerOutlet> customerShippingAddresses = request.Customer.CustomerOutletAddresses
            .Select(s => CustomerOutlet.Create(
                result.Value.Id,
                s.LocationName,
                s.FullName,
                s.Email,
                s.PhoneNumber,
                s.Mobile,
                s.AddressLine1,
                s.AddressLine2,
                s.TownCity,
                s.County,
                s.Country,
                s.PostalCode,
                s.DeliveryNote,
                s.DeliveryTimeFrom,
                s.DeliveryTimeTo,
                s.Is24HrsDelivery,
                request.UserId,
                dateTimeProvider.UtcNow));

        customerShippingAddressRepository.InsertRange(customerShippingAddresses);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
