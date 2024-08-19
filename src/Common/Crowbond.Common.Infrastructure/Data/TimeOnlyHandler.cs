using System.Data;
using Dapper;

namespace Crowbond.Common.Infrastructure.Data;

internal sealed class TimeOnlyHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.Value = value.ToTimeSpan();
        parameter.DbType = DbType.Time;
    }

    public override TimeOnly Parse(object value)
    {
        if (value is TimeSpan timeSpan)
        {
            return TimeOnly.FromTimeSpan(timeSpan);
        }

        throw new InvalidCastException($"Cannot convert {value} to TimeOnly.");
    }
}
