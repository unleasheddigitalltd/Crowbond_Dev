using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletDetails;

internal sealed class GetCustomerOutletDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerOutletDetailsQuery, CustomerOutletDetailsResponse>
{
    public async Task<Result<CustomerOutletDetailsResponse>> Handle(GetCustomerOutletDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerOutletDetailsResponse.Id)},
                 customer_id AS {nameof(CustomerOutletDetailsResponse.CustomerId)},
                 location_name AS {nameof(CustomerOutletDetailsResponse.LocationName)}, 
                 full_name AS {nameof(CustomerOutletDetailsResponse.FullName)}, 
                 email AS {nameof(CustomerOutletDetailsResponse.Email)}, 
                 phone_number AS {nameof(CustomerOutletDetailsResponse.PhoneNumber)}, 
                 mobile AS {nameof(CustomerOutletDetailsResponse.Mobile)}, 
                 address_line1 AS {nameof(CustomerOutletDetailsResponse.AddressLine1)}, 
                 address_line2 AS {nameof(CustomerOutletDetailsResponse.AddressLine2)}, 
                 town_city AS {nameof(CustomerOutletDetailsResponse.TownCity)}, 
                 county AS {nameof(CustomerOutletDetailsResponse.County)}, 
                 country AS {nameof(CustomerOutletDetailsResponse.Country)}, 
                 postal_code AS {nameof(CustomerOutletDetailsResponse.PostalCode)}, 
                 delivery_note AS {nameof(CustomerOutletDetailsResponse.DeliveryNote)}, 
                 delivery_time_from AS {nameof(CustomerOutletDetailsResponse.DeliveryTimeFrom)}, 
                 delivery_time_to AS {nameof(CustomerOutletDetailsResponse.DeliveryTimeTo)}, 
                 is24hrs_delivery AS {nameof(CustomerOutletDetailsResponse.Is24HrsDelivery)}
             FROM crm.customer_outlets
             WHERE id = @CustomerOutletId            
             """;

        CustomerOutletDetailsResponse? customerOutlet = await connection.QuerySingleOrDefaultAsync<CustomerOutletDetailsResponse>(sql, request);

        if (customerOutlet is null)
        {
            return Result.Failure<CustomerOutletDetailsResponse>(CustomerOutletErrors.NotFound(request.CustomerOutletId));
        }

        return customerOutlet;
    }
}
