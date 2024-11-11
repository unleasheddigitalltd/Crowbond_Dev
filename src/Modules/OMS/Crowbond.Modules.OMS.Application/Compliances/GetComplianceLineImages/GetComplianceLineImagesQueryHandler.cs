using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Compliances.GetComplianceLineImages;

internal sealed class GetComplianceLineImagesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetComplianceLineImagesQuery, IReadOnlyCollection<ComplianceLineImageResponse>>
{
    public async Task<Result<IReadOnlyCollection<ComplianceLineImageResponse>>> Handle(GetComplianceLineImagesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 li.id AS {nameof(ComplianceLineImageResponse.Id)},
                 li.image_name AS {nameof(ComplianceLineImageResponse.ImageName)}
             FROM oms.compliance_line_images li
             INNER JOIN oms.compliance_lines l ON l.id = li.compliance_line_id
             WHERE l.id = @ComplianceLineId
             """;

        List<ComplianceLineImageResponse> complianceLineImages = (await connection.QueryAsync<ComplianceLineImageResponse>(sql, request)).AsList();

        return complianceLineImages;
    }
}
