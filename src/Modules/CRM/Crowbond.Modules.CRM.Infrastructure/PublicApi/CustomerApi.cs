using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrder;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrder;
using Crowbond.Modules.CRM.PublicApi;
using MediatR;
using CustomerForOrderResponse = Crowbond.Modules.CRM.PublicApi.CustomerForOrderResponse;
using CustomerOutletForOrderResponse = Crowbond.Modules.CRM.PublicApi.CustomerOutletForOrderResponse;

namespace Crowbond.Modules.CRM.Infrastructure.PublicApi;

internal sealed class CustomerApi(ISender sender) : ICustomerApi
{
    public async Task<CustomerForOrderResponse?> GetForOrderAsync(Guid contactId, CancellationToken cancellationToken = default)
    {
        Result<Application.Customers.GetCustomerForOrder.CustomerForOrderResponse> result =
            await sender.Send(new GetCustomerForOrderQuery(contactId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
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
            result.Value.PaymentTerms,
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
