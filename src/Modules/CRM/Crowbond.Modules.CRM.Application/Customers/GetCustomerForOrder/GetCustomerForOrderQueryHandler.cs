﻿using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;
using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Settings;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrder;

internal sealed class GetCustomerForOrderQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerForOrderQuery, CustomerForOrderResponse>
{
    public async Task<Result<CustomerForOrderResponse>> Handle(GetCustomerForOrderQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sql =
            $"""
             SELECT
                id AS {nameof(CustomerForOrderResponse.Id)},
                account_number AS {nameof(CustomerForOrderResponse.AccountNumber)},
                business_name AS {nameof(CustomerForOrderResponse.BusinessName)},
                price_tier_id AS {nameof(CustomerForOrderResponse.PriceTierId)},
                discount AS {nameof(CustomerForOrderResponse.Discount)},
                custom_payment_terms AS {nameof(CustomerForOrderResponse.CustomPaymentTerms)},
                due_date_calculation_basis AS {nameof(CustomerForOrderResponse.DueDateCalculationBasis)},  
                due_days_for_invoice AS {nameof(CustomerForOrderResponse.DueDaysForInvoice)},            
                delivery_fee_setting AS {nameof(CustomerForOrderResponse.DeliveryFeeSetting)},
                delivery_min_order_value AS {nameof(CustomerForOrderResponse.DeliveryMinOrderValue)},
                delivery_charge AS {nameof(CustomerForOrderResponse.DeliveryCharge)},
                no_discount_special_item AS {nameof(CustomerForOrderResponse.NoDiscountSpecialItem)},
                no_discount_fixed_price AS {nameof(CustomerForOrderResponse.NoDiscountFixedPrice)},
                detailed_invoice AS {nameof(CustomerForOrderResponse.DetailedInvoice)},
                customer_notes AS {nameof(CustomerForOrderResponse.CustomerNotes)}
             FROM crm.customers
             WHERE id = @CustomerId AND is_active = true;

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
            return Result.Failure<CustomerForOrderResponse>(CustomerErrors.NotFound(request.CustomerId));
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

        return Result.Success(customer);
    }
}
