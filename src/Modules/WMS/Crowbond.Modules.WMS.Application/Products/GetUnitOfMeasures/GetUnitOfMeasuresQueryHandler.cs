﻿using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetUnitOfMeasures;

internal sealed class GetUnitOfMeasuresQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUnitOfMeasuresQuery, IReadOnlyCollection<UnitOfMeasureResponse>>
{
    public async Task<Result<IReadOnlyCollection<UnitOfMeasureResponse>>> Handle(GetUnitOfMeasuresQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 name AS {nameof(UnitOfMeasureResponse.Name)}
             FROM wms.unit_of_measures
             """;

        List<UnitOfMeasureResponse> unitOfMeasures = (await connection.QueryAsync<UnitOfMeasureResponse>(sql, request)).AsList();

        return unitOfMeasures;
    }
}
