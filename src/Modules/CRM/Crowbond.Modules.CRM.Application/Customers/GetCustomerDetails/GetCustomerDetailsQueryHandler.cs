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
             WITH FilteredCustomers AS (
                SELECT
                    c.id AS {nameof(CustomerDetailsResponse.Id)}, 
                    c.account_number AS {nameof(CustomerDetailsResponse.AccountNumber)},  
                    c.business_name AS {nameof(CustomerDetailsResponse.BusinessName)},  
                    c.billing_address_line1 AS {nameof(CustomerDetailsResponse.BillingAddressLine1)},  
                    c.billing_address_line2 AS {nameof(CustomerDetailsResponse.BillingAddressLine2)},  
                    c.billing_town_city AS {nameof(CustomerDetailsResponse.BillingTownCity)},  
                    c.billing_county AS {nameof(CustomerDetailsResponse.BillingCounty)},  
                    c.billing_country AS {nameof(CustomerDetailsResponse.BillingCountry)},  
                    c.billing_postal_code AS {nameof(CustomerDetailsResponse.BillingPostalCode)},  
                    c.price_tier_id AS {nameof(CustomerDetailsResponse.PriceTierId)},  
                    c.discount AS {nameof(CustomerDetailsResponse.Discount)},  
                    c.rep_id AS {nameof(CustomerDetailsResponse.RepId)},  
                    c.custom_payment_terms AS {nameof(CustomerDetailsResponse.CustomPaymentTerms)},                      
                    c.due_date_calculation_basis AS {nameof(CustomerDetailsResponse.DueDateCalculationBasis)},  
                    c.due_days_for_invoice AS {nameof(CustomerDetailsResponse.DueDaysForInvoice)},  
                    c.delivery_fee_setting AS {nameof(CustomerDetailsResponse.DeliveryFeeSetting)},  
                    c.delivery_min_order_value AS {nameof(CustomerDetailsResponse.DeliveryMinOrderValue)},  
                    c.delivery_charge AS {nameof(CustomerDetailsResponse.DeliveryCharge)},  
                    c.no_discount_special_item AS {nameof(CustomerDetailsResponse.NoDiscountSpecialItem)},  
                    c.no_discount_fixed_price AS {nameof(CustomerDetailsResponse.NoDiscountFixedPrice)},  
                    c.detailed_invoice AS {nameof(CustomerDetailsResponse.DetailedInvoice)},  
                    c.customer_notes AS {nameof(CustomerDetailsResponse.CustomerNotes)},  
                    c.is_hq AS {nameof(CustomerDetailsResponse.IsHq)},  
                    c.is_active AS {nameof(CustomerDetailsResponse.IsActive)},
                    t.first_name AS {nameof(CustomerDetailsResponse.FirstName)},
                    t.last_name AS {nameof(CustomerDetailsResponse.LastName)},
                    t.phone_number AS {nameof(CustomerDetailsResponse.PhoneNumber)},
                    t.email AS {nameof(CustomerDetailsResponse.Email)},
                    cs.show_prices_in_delivery_docket AS {nameof(CustomerDetailsResponse.ShowPricesInDeliveryDocket)},  
                    cs.show_price_in_app AS {nameof(CustomerDetailsResponse.ShowPriceInApp)}, 
                    cs.show_logo_in_delivery_docket AS {nameof(CustomerDetailsResponse.ShowLogoInDeliveryDocket)}, 
                    cs.customer_logo AS {nameof(CustomerDetailsResponse.CustomerLogo)},
                    ROW_NUMBER() OVER (PARTITION BY c.id ORDER BY t.is_primary DESC) AS FilterRowNum
                FROM crm.customers c
                INNER JOIN crm.customer_settings cs ON c.id = cs.customer_id
                LEFT JOIN crm.customer_contacts t ON c.id = t.customer_id
                WHERE c.id = @CustomerId
             )
             SELECT * FROM FilteredCustomers  WHERE FilterRowNum = 1;

             SELECT
                 id AS {nameof(CustomerContactResponse.Id)},
                 customer_id AS {nameof(CustomerContactResponse.CustomerId)},
                 first_name AS {nameof(CustomerContactResponse.FirstName)},
                 last_name AS {nameof(CustomerContactResponse.LastName)},
                 phone_number AS {nameof(CustomerContactResponse.PhoneNumber)},
                 email AS {nameof(CustomerContactResponse.Email)},
                 is_primary AS {nameof(CustomerContactResponse.IsPrimary)},
                 is_active AS {nameof(CustomerContactResponse.IsActive)}
             FROM crm.customer_contacts
             WHERE customer_id = @CustomerId
             ORDER BY is_primary DESC, is_active DESC;

             SELECT
                 id AS {nameof(CustomerOutletResponse.Id)},
                 customer_id AS {nameof(CustomerOutletResponse.CustomerId)},
                 location_name AS {nameof(CustomerOutletResponse.LocationName)},
                 address_line1 AS {nameof(CustomerOutletResponse.AddressLine1)},
                 address_line2 AS {nameof(CustomerOutletResponse.AddressLine2)},
                 town_city AS {nameof(CustomerOutletResponse.TownCity)},
                 county AS {nameof(CustomerOutletResponse.County)},
                 country AS {nameof(CustomerOutletResponse.Country)},
                 postal_code AS {nameof(CustomerOutletResponse.PostalCode)}, 
                 is_active AS {nameof(CustomerOutletResponse.IsActive)}      
             FROM crm.customer_outlets
             WHERE customer_id = @CustomerId
             ORDER BY is_active DESC;
             """;


        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customers = (await multi.ReadAsync<CustomerDetailsResponse>()).ToList();
        var customerContacts = (await multi.ReadAsync<CustomerContactResponse>()).ToList();
        var customerOutlets = (await multi.ReadAsync<CustomerOutletResponse>()).ToList();

        CustomerDetailsResponse? customer = customers.SingleOrDefault();

        if (customer is null)
        {
            return Result.Failure<CustomerDetailsResponse>(CustomerErrors.NotFound(request.CustomerId));
        }

        customer.CustomerContacts = customerContacts.Where(a => a.CustomerId == customer.Id).ToList();
        customer.CustomerOutlets = customerOutlets.Where(a => a.CustomerId == customer.Id).ToList();

        return customer;
    }
}
