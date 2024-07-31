using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutlet;

internal sealed class GetCustomerOutletQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerOutletQuery, CustomerOutletResponse>
{
    public async Task<Result<CustomerOutletResponse>> Handle(GetCustomerOutletQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 SELECT
                 s.id AS {nameof(CustomerOutletResponse.Id)},
                 s.customer_id AS {nameof(CustomerOutletResponse.CustomerId)},
                 s.location_name AS {nameof(CustomerOutletResponse.LocationName)},
                 s.address_line1 AS {nameof(CustomerOutletResponse.AddressLine1)},
                 s.address_line2 AS {nameof(CustomerOutletResponse.AddressLine2)},
                 s.town_city AS {nameof(CustomerOutletResponse.TownCity)},
                 s.county AS {nameof(CustomerOutletResponse.County)},
                 s.country AS {nameof(CustomerOutletResponse.Country)},
                 s.postal_code AS {nameof(CustomerOutletResponse.PostalCode)},
             FROM crm.customer_outlets t
             WHERE id = @CustomerOutletId
             """;

        CustomerOutletResponse? customerOutlet = await connection.QuerySingleOrDefaultAsync<CustomerOutletResponse>(sql, request);

        if (customerOutlet is null)
        {
            return Result.Failure<CustomerOutletResponse>(CustomerOutletErrors.NotFound(request.CustomerOutletId));
        }

        return customerOutlet;
    }
}
