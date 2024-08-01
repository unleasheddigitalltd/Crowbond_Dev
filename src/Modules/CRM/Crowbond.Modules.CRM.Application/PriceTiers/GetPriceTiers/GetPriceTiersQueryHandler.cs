using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;
using System.Data.Common;

namespace Crowbond.Modules.CRM.Application.PriceTiers.GetPriceTiers;

internal sealed class GetPriceTiersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPriceTiersQuery, IReadOnlyCollection<PriceTierResponse>>
{
    public async Task<Result<IReadOnlyCollection<PriceTierResponse>>> Handle(GetPriceTiersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(PriceTierResponse.Id)},
                 name AS {nameof(PriceTierResponse.Name)}
             FROM crm.price_tiers
             """;

        List<PriceTierResponse> priceTiers = (await connection.QueryAsync<PriceTierResponse>(sql, request)).AsList();

        return priceTiers;
    }
}
