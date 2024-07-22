using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerContactRepository customerContactRepository,
    ICustomerOutletRepository customerShippingAddressRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCustomerCommand>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

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
            showPricesInDeliveryDocket: request.Customer.ShowPricesInDeliveryDocket,
            showPriceInApp: request.Customer.ShowPriceInApp,
            detailedInvoice: request.Customer.DetailedInvoice,
            customerNotes: request.Customer.CustomerNotes,
            isHq: request.Customer.IsHq,
            isActive: request.Customer.IsActive,
            lastModifiedBy: request.UserName,
            lastModifiedDate: dateTimeProvider.UtcNow);

        IEnumerable<CustomerContact> customerContacts = await customerContactRepository.GetForCustomerAsync(customer, cancellationToken);

        foreach (CustomerContact contact in customerContacts)
        {
            contact.Update(
                firstName: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.FirstName ?? "",
                lastName: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.LastName ?? "",
                phoneNumber: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.PhoneNumber ?? "",
                mobile: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.Mobile ?? "",
                primary: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.Primary ?? false,
                receiveInvoice: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact?.Id)?.ReceiveInvoice ?? false,
                receiveOrder: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.ReceiveOrder ?? false,
                receivePriceList: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.ReceivePriceList ?? false,
                isActive: request.Customer.CustomerContacts.SingleOrDefault(t => t.Id == contact.Id)?.IsActive ?? false,
                lastModifiedBy: request.UserName,
                lastModifiedDate: dateTimeProvider.UtcNow);
        }        

        IEnumerable<CustomerOutlet> customerShippingAddresses = await customerShippingAddressRepository.GetForCustomerAsync(customer, cancellationToken);

        foreach(CustomerOutlet address in customerShippingAddresses)
        {
            address.Update(
                locationName: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id) ?.LocationName ??"",
                fullName: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id) ?.FullName ?? "",
                email: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id) ?.Email ?? "",
                phoneNumber: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id) ?.PhoneNumber ?? "",
                mobile: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id) ?.Mobile ?? "",
                addressLine1 : request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.AddressLine1 ?? "",
                addressLine2: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.AddressLine2,
                townCity: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.TownCity ?? "",
                county: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.County ?? "",
                country: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.Country,
                postalCode: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.PostalCode ?? "",
                deliveryNote: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.DeliveryNote,
                deliveryTimeFrom: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.DeliveryTimeFrom ?? dateTimeProvider.UtcNow,
                deliveryTimeTo: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.DeliveryTimeTo ?? dateTimeProvider.UtcNow,
                is24HrsDelivery: request.Customer.CustomerOutletAddresses.SingleOrDefault(s => s.Id == address.Id)?.Is24HrsDelivery ?? false,
                lastModifiedBy: request.UserName,
                lastModifiedDate: dateTimeProvider.UtcNow);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
