using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Reps.GetReps;

internal sealed class GetRepsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRepsQuery, IReadOnlyCollection<RepResponse>>
{
    public async Task<Result<IReadOnlyCollection<RepResponse>>> Handle(GetRepsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(RepResponse.Id)},
                 name AS {nameof(RepResponse.Name)}
             FROM crm.reps
             """;

        List<RepResponse> reps = (await connection.QueryAsync<RepResponse>(sql, request)).AsList();

        return reps;
    }
}
