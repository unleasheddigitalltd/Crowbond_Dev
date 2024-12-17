using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Compliances;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Compliances.GetCompliance;

internal sealed class GetComplianceQueryHandler(
    IDateTimeProvider dateTimeProvider,
    IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetComplianceQuery, ComplianceResponse>
{
    public async Task<Result<ComplianceResponse>> Handle(GetComplianceQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 c.id AS {nameof(ComplianceResponse.Id)},
                 v.vehicle_regn AS {nameof(ComplianceResponse.VehicleRegn)},
                 u.first_name AS {nameof(ComplianceResponse.DriverFirstName)},
                 u.last_name AS {nameof(ComplianceResponse.DriverLastName)},
                 c.form_no AS {nameof(ComplianceResponse.FormNo)},
                 c.form_date AS {nameof(ComplianceResponse.FormDate)},
                 cl.id AS {nameof(ComplianceLineResponse.ComplianceLineId)},
                 cl.compliance_header_id AS {nameof(ComplianceLineResponse.ComplianceId)},
                 cq.text AS {nameof(ComplianceLineResponse.QuestionText)},
                 cl.response AS {nameof(ComplianceLineResponse.Response)},
                 cl.description AS {nameof(ComplianceLineResponse.Description)}
             FROM oms.drivers d
             INNER JOIN oms.users u ON u.id = d.id
             INNER JOIN oms.route_trip_logs l ON d.id = l.driver_id
             INNER JOIN oms.compliance_headers c ON l.id = c.route_trip_log_id
             INNER JOIN oms.vehicles v ON v.id = c.vehicle_id
             INNER JOIN oms.compliance_lines cl ON cl.compliance_header_id = c.id
             INNER JOIN oms.compliance_questions cq ON cq.id = cl.compliance_question_id
             WHERE d.id = @DriverId 
               AND l.logged_off_time IS NULL 
               AND l.logged_on_time::DATE = @CurrentDate 
             """;

        var parameters = new { request.DriverId, CurrentDate = DateOnly.FromDateTime(dateTimeProvider.UtcNow) };

        Dictionary<Guid, ComplianceResponse> complianceDictionary = [];
        await connection.QueryAsync<ComplianceResponse, ComplianceLineResponse, ComplianceResponse>(
            sql,
            (compliance, complianceLine) =>
            {
                if (complianceDictionary.TryGetValue(compliance.Id, out ComplianceResponse? existingCompliance))
                {
                    compliance = existingCompliance;
                }
                else
                {
                    complianceDictionary.Add(compliance.Id, compliance);
                }

                compliance.ComplianceLines.Add(complianceLine);

                return compliance;
            },
            parameters,
            splitOn: nameof(ComplianceLineResponse.ComplianceLineId));

        if (complianceDictionary.Count == 0)
        {
            return Result.Failure<ComplianceResponse>(ComplianceErrors.ForDriverNotFound(request.DriverId));
        }

        return Result.Success(complianceDictionary.Values.First());
    }
}
