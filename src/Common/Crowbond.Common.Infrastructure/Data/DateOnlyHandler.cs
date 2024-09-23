using System.Data;
using Dapper;

namespace Crowbond.Common.Infrastructure.Data;

internal sealed class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        parameter.DbType = DbType.Date;
    }

    public override DateOnly Parse(object value)
    {
        if (value is DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        throw new InvalidCastException($"Cannot convert {value} to DateOnly.");
    }
}
