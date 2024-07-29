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
                 c.id, 
                 c.account_number, 
                 c.business_name, 
                 c.billing_address_line1, 
                 c.billing_address_line2, 
                 c.billing_town_city, 
                 c.billing_county, 
                 c.billing_country, 
                 c.billing_postal_code, 
                 t.name, 
                 c.discount, 
                 r.name, 
                 c.custom_payment_term, 
                 c.payment_terms, 
                 c.invoice_due_days, 
                 c.delivery_fee_setting, 
                 c.delivery_min_order_value, 
                 c.delivery_charge, 
                 c.no_discount_special_item, 
                 c.no_discount_fixed_price, 
                 c.detailed_invoice, 
                 c.customer_notes, 
                 c.is_hq, 
                 c.is_active
                 cs.show_prices_in_delivery_docket, 
                 cs.show_price_in_app,
                 cs.show_logo_on_delivery_docket,
                 cs.customer_logo
             FROM crm.customers c
             INNER JOIN crm.customer_settings cs ON c.id = cs.customerId
             INNER JOIN crm.price_tiers t ON t.id = c.price_tier_id
             INNER JOIN crm.reps r ON r.id = c.rep_id
             WHERE c.id = @CustomerId;

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
                 s.address_line1 AS {nameof(CustomerShippingAddressResponse.ShippingAddressLine1)},
                 s.address_line2 AS {nameof(CustomerShippingAddressResponse.ShippingAddressLine2)},
                 s.town_city AS {nameof(CustomerShippingAddressResponse.ShippingTownCity)},
                 s.county AS {nameof(CustomerShippingAddressResponse.ShippingCounty)},
                 s.country AS {nameof(CustomerShippingAddressResponse.ShippingCountry)},
                 s.postal_code AS {nameof(CustomerShippingAddressResponse.ShippingPostalCode)},
                 s.delivery_note AS {nameof(CustomerShippingAddressResponse.DeliveryNote)},
                 s.delivery_time_from AS {nameof(CustomerShippingAddressResponse.DeliveryTimeFrom)},
                 s.delivery_time_to AS {nameof(CustomerShippingAddressResponse.DeliveryTimeTo)},
                 s.is24hrs_delivery AS {nameof(CustomerShippingAddressResponse.Is24HrsDelivery)}                
             FROM crm.customer_outlets s
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
