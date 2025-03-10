using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrder;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerByAccountNumber;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrder;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrderByContactId;
using Crowbond.Modules.CRM.PublicApi;
using MediatR;
using CustomerForOrderResponse = Crowbond.Modules.CRM.PublicApi.CustomerForOrderResponse;
using CustomerOutletForOrderResponse = Crowbond.Modules.CRM.PublicApi.CustomerOutletForOrderResponse;

namespace Crowbond.Modules.CRM.Infrastructure.PublicApi;

internal sealed class CustomerApi(ISender sender) : ICustomerApi
{
    public async Task<CustomerForOrderResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Result<Application.Customers.GetCustomerForOrder.CustomerForOrderResponse> result =
            await sender.Send(new GetCustomerForOrderQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        if (result.Value.DueDateCalculationBasis == null)
        {
            throw new InvalidOperationException("DueDateCalculationBasis cannot be null.");
        }

        if (result.Value.DueDaysForInvoice == null)
        {
            throw new InvalidOperationException("DueDaysForInvoice cannot be null.");
        }

        return new CustomerForOrderResponse(
            result.Value.Id,
            result.Value.AccountNumber,
            result.Value.BusinessName,
            result.Value.PriceTierId,
            result.Value.Discount,
            result.Value.NoDiscountSpecialItem,
            result.Value.NoDiscountFixedPrice,
            result.Value.DetailedInvoice,
            (int)result.Value.DueDateCalculationBasis,
            (int)result.Value.DueDaysForInvoice,
            result.Value.CustomerNotes,
            result.Value.DeliveryFeeSetting,
            result.Value.DeliveryMinOrderValue,
            result.Value.DeliveryCharge);
    }

    public async Task<CustomerForOrderResponse?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        Result<Application.Customers.GetCustomerByAccountNumber.CustomerForOrderResponse> result = await sender.Send(new GetCustomerByAccountNumberQuery(accountNumber), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        if (result.Value.DueDateCalculationBasis == null)
        {
            throw new InvalidOperationException("DueDateCalculationBasis cannot be null.");
        }

        if (result.Value.DueDaysForInvoice == null)
        {
            throw new InvalidOperationException("DueDaysForInvoice cannot be null.");
        }

        return new CustomerForOrderResponse(
            result.Value.Id,
            result.Value.AccountNumber,
            result.Value.BusinessName,
            result.Value.PriceTierId,
            result.Value.Discount,
            result.Value.NoDiscountSpecialItem,
            result.Value.NoDiscountFixedPrice,
            result.Value.DetailedInvoice,
            (int)result.Value.DueDateCalculationBasis,
            (int)result.Value.DueDaysForInvoice,
            result.Value.CustomerNotes,
            result.Value.DeliveryFeeSetting,
            result.Value.DeliveryMinOrderValue,
            result.Value.DeliveryCharge);
    }

    public async Task<CustomerForOrderResponse?> GetByContactIdAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        Result<Application.Customers.GetCustomerForOrderByContactId.CustomerForOrderResponse> result =
            await sender.Send(new GetCustomerForOrderByContactIdQuery(contactId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        if (result.Value.DueDateCalculationBasis == null)
        {
            throw new InvalidOperationException("DueDateCalculationBasis cannot be null.");
        }

        if (result.Value.DueDaysForInvoice == null)
        {
            throw new InvalidOperationException("DueDaysForInvoice cannot be null.");
        }

        return new CustomerForOrderResponse(
            result.Value.Id,
            result.Value.AccountNumber,
            result.Value.BusinessName,
            result.Value.PriceTierId,
            result.Value.Discount,
            result.Value.NoDiscountSpecialItem,
            result.Value.NoDiscountFixedPrice,
            result.Value.DetailedInvoice,
            (int)result.Value.DueDateCalculationBasis,
            (int)result.Value.DueDaysForInvoice,
            result.Value.CustomerNotes,
            result.Value.DeliveryFeeSetting,
            result.Value.DeliveryMinOrderValue,
            result.Value.DeliveryCharge);
    }

    public async Task<CustomerOutletForOrderResponse?> GetOutletForOrderAsync(Guid outletId, CancellationToken cancellationToken = default)
    {
        Result<Application.CustomerOutlets.GetCustomerOutletForOrder.CustomerOutletForOrderResponse> result =
            await sender.Send(new GetCustomerOutletForOrderQuery(outletId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }
        return new CustomerOutletForOrderResponse(
            result.Value.Id,
            result.Value.CustomerId,
            result.Value.LocationName,
            result.Value.FullName,
            result.Value.Email,
            result.Value.PhoneNumber,
            result.Value.Mobile,
            result.Value.AddressLine1,
            result.Value.AddressLine2,
            result.Value.TownCity,
            result.Value.County,
            result.Value.Country,
            result.Value.PostalCode,
            result.Value.DeliveryNote,
            result.Value.DeliveryTimeFrom,
            result.Value.DeliveryTimeTo,
            result.Value.Is24HrsDelivery);
    }
}
