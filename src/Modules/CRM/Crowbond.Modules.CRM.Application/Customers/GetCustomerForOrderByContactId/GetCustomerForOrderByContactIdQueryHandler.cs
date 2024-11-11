using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Settings;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrderByContactId;

internal sealed class GetCustomerForOrderQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerForOrderByContactIdQuery, CustomerForOrderResponse>
{
    public async Task<Result<CustomerForOrderResponse>> Handle(GetCustomerForOrderByContactIdQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sql =
            $"""
             SELECT
                c.id AS {nameof(CustomerForOrderResponse.Id)},
                c.account_number AS {nameof(CustomerForOrderResponse.AccountNumber)},
                c.business_name AS {nameof(CustomerForOrderResponse.BusinessName)},
                c.price_tier_id AS {nameof(CustomerForOrderResponse.PriceTierId)},
                c.discount AS {nameof(CustomerForOrderResponse.Discount)},
                c.custom_payment_terms AS {nameof(CustomerForOrderResponse.CustomPaymentTerms)},
                c.due_date_calculation_basis AS {nameof(CustomerForOrderResponse.DueDateCalculationBasis)},  
                c.due_days_for_invoice AS {nameof(CustomerForOrderResponse.DueDaysForInvoice)},            
                c.delivery_fee_setting AS {nameof(CustomerForOrderResponse.DeliveryFeeSetting)},
                c.delivery_min_order_value AS {nameof(CustomerForOrderResponse.DeliveryMinOrderValue)},
                c.delivery_charge AS {nameof(CustomerForOrderResponse.DeliveryCharge)},
                c.no_discount_special_item AS {nameof(CustomerForOrderResponse.NoDiscountSpecialItem)},
                c.no_discount_fixed_price AS {nameof(CustomerForOrderResponse.NoDiscountFixedPrice)},
                c.detailed_invoice AS {nameof(CustomerForOrderResponse.DetailedInvoice)},
                c.customer_notes AS {nameof(CustomerForOrderResponse.CustomerNotes)}
             FROM crm.customers c
             INNER JOIN crm.customer_contacts cc ON c.id = cc.customer_id
             WHERE cc.is_active = true AND c.is_active = true AND cc.id = @ContactId;
             
             SELECT
                 payment_terms AS {nameof(PaymentSetting.PaymentTerms)}
             FROM crm.settings
             WHERE is_deleted = false
             LIMIT 1;
             """;


        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        CustomerForOrderResponse? customer = await multi.ReadSingleAsync<CustomerForOrderResponse>();
        PaymentSetting paymentSetting = await multi.ReadSingleAsync<PaymentSetting>();

        if (customer is null)
        {
            return Result.Failure<CustomerForOrderResponse>(CustomerContactErrors.NotFound(request.ContactId));
        }

        if (!customer.CustomPaymentTerms)
        {
            if (paymentSetting is null)
            {
                return Result.Failure<CustomerForOrderResponse>(SettingErrors.NotFound);
            }

            customer.DueDaysForInvoice = Setting.GetDueDaysForInvoice(paymentSetting.PaymentTerms);
            customer.DueDateCalculationBasis = Setting.GetDueDateCalculationBasis(paymentSetting.PaymentTerms);
        }


        if (customer.DueDateCalculationBasis is null || customer.DueDaysForInvoice is null)
        {
            return Result.Failure<CustomerForOrderResponse>(CustomerErrors.CustomPaymentTermsParametersHasNoValue);
        }

        return customer;
    }
}
