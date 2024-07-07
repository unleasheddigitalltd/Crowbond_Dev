namespace Crowbond.Common.Application.Extentions;
public static class EnumExtensions
{
    public static bool TryConvertToEnum<TEnum>(this string value, out TEnum result) where TEnum : struct
    {
        return Enum.TryParse<TEnum>(value, true, out result);
    }
}
