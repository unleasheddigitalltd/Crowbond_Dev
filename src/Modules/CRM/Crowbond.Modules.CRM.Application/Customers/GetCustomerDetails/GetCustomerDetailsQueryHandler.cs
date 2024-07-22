using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Customers;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;

internal sealed class GetCustomerDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerDetailsQuery, CustomerDetailsResponse>
{
    public async Task<Result<CustomerDetailsResponse>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerDetailsResponse.Id)},
                 account_number AS {nameof(CustomerDetailsResponse.AccountNumber)},
                 business_name AS {nameof(CustomerDetailsResponse.BusinessName)},
                 billing_address_line1 AS {nameof(CustomerDetailsResponse.BillingAddressLine1)},
                 billing_address_line2 AS {nameof(CustomerDetailsResponse.BillingAddressLine2)},
                 billing_town_city AS {nameof(CustomerDetailsResponse.BillingTownCity)},
                 billing_county AS {nameof(CustomerDetailsResponse.BillingCounty)},
                 billing_country AS {nameof(CustomerDetailsResponse.BillingCountry)},
                 billing_postal_code AS {nameof(CustomerDetailsResponse.BillingPostalCode)},
                 payment_terms AS {nameof(CustomerDetailsResponse.PaymentTerms)},
                 detailed_invoice AS {nameof(CustomerDetailsResponse.DetailedInvoice)},
                 price_group_id AS {nameof(CustomerDetailsResponse.PriceGroupId)},
                 invoice_period_id AS {nameof(CustomerDetailsResponse.InvoicePeriodId)},
                 customer_notes AS {nameof(CustomerDetailsResponse.CustomerNotes)},
                 is_hq AS {nameof(CustomerDetailsResponse.IsHq)},
                 is_active AS {nameof(CustomerDetailsResponse.IsActive)}
             FROM crm.customers
             WHERE id = @CustomerId;

             SELECT
                 t.id AS {nameof(CustomerContactResponse.Id)},
                 t.customer_id AS {nameof(CustomerContactResponse.CustomerId)},
                 t.first_name AS {nameof(CustomerContactResponse.FirstName)},
                 t.last_name AS {nameof(CustomerContactResponse.LastName)},
                 t.phone_number AS {nameof(CustomerContactResponse.PhoneNumber)},
                 t.mobile AS {nameof(CustomerContactResponse.Mobile)},
                 t.email AS {nameof(CustomerContactResponse.Email)},
                 t.primary AS {nameof(CustomerContactResponse.Primary)},
                 t.is_active AS {nameof(CustomerContactResponse.IsActive)}
             FROM crm.customer_contacts t
             INNER JOIN crm.customers c ON c.id = t.customer_id
             WHERE c.id = @CustomerId;

             SELECT
                 s.id AS {nameof(CustomerShippingAddressResponse.Id)},
                 s.customer_id AS {nameof(CustomerShippingAddressResponse.CustomerId)},
                 s.shipping_address_line1 AS {nameof(CustomerShippingAddressResponse.ShippingAddressLine1)},
                 s.shipping_address_line2 AS {nameof(CustomerShippingAddressResponse.ShippingAddressLine2)},
                 s.shipping_town_city AS {nameof(CustomerShippingAddressResponse.ShippingTownCity)},
                 s.shipping_county AS {nameof(CustomerShippingAddressResponse.ShippingCounty)},
                 s.shipping_country AS {nameof(CustomerShippingAddressResponse.ShippingCountry)},
                 s.shipping_postal_code AS {nameof(CustomerShippingAddressResponse.ShippingPostalCode)},
                 s.delivery_note AS {nameof(CustomerShippingAddressResponse.DeliveryNote)},
                 s.delivery_time_from AS {nameof(CustomerShippingAddressResponse.DeliveryTimeFrom)},
                 s.delivery_time_to AS {nameof(CustomerShippingAddressResponse.DeliveryTimeTo)},
                 s.is24hrs_delivery AS {nameof(CustomerShippingAddressResponse.Is24HrsDelivery)}                
             FROM crm.customer_shipping_addresses s
             INNER JOIN crm.customers c ON c.id = s.customer_id
             WHERE c.id = @CustomerId;
             """;


        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customers = (await multi.ReadAsync<CustomerDetailsResponse>()).ToList();
        var customerContacts = (await multi.ReadAsync<CustomerContactResponse>()).ToList();
        var customerShippingAddresses = (await multi.ReadAsync<CustomerShippingAddressResponse>()).ToList();

        CustomerDetailsResponse? customer = customers.SingleOrDefault();

        if (customer is null)
        {
            return Result.Failure<CustomerDetailsResponse>(CustomerErrors.NotFound(request.CustomerId));
        }

        customer.CustomerContacts = customerContacts.Where(a => a.CustomerId == customer.Id).ToList();
        customer.CustomerShippingAddresses = customerShippingAddresses.Where(a => a.CustomerId == customer.Id).ToList();

        return customer;
    }
}
