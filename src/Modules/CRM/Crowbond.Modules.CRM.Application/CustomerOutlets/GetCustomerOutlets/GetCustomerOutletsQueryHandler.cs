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
                 s.id AS {nameof(CustomerOutletResponse.Id)},
                 s.customer_id AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.location_name AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.address_line1 AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.address_line2 AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.town_city AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.county AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.country AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.postal_code AS {nameof(CustomerOutletResponse.CustomerId)},         
             FROM crm.customer_outlets s
             INNER JOIN crm.customers c ON c.id = s.customer_id
             WHERE c.id = @CustomerId;
             """;

        List<CustomerOutletResponse> customerOutlets = (await connection.QueryAsync<CustomerOutletResponse>(sql, request)).AsList();

        return customerOutlets;
    }
}
