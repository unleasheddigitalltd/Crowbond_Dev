using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutlets;

internal sealed class GetCustomerOutletsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerOutletsQuery, IReadOnlyCollection<CustomerOutletResponse>>
{
    public async Task<Result<IReadOnlyCollection<CustomerOutletResponse>>> Handle(GetCustomerOutletsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
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

        List<CustomerOutletResponse> customerOutlets = (await connection.QueryAsync<CustomerOutletResponse>(sql, request)).AsList();

        return customerOutlets;
    }
}
