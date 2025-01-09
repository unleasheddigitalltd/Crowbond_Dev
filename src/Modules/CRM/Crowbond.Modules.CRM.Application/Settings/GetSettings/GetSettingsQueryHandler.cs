using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.Settings;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Settings.GetSettings;

internal sealed class GetSettingsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSettingsQuery, SettingsResponse>
{
    public async Task<Result<SettingsResponse>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 payment_terms AS {nameof(SettingsResponse.PaymentTerms)}
             FROM crm.settings
             WHERE is_deleted = false
             """;

        SettingsResponse? settings = await connection.QuerySingleOrDefaultAsync<SettingsResponse>(sql, request);

        if (settings is null)
        {
            return Result.Failure<SettingsResponse>(SettingErrors.NotFound);
        }

        return settings;
    }
}
