using System.Globalization;
using Core.Constants;
using Core.Constants.Message;

namespace Core.Extensions;

public static class Extensions
{
    public static (string, string?) Split(this decimal number)
    {
        var splitDecimal = number.ToString(CultureInfo.InvariantCulture).Split(Constant.DecimalPoint);
        return splitDecimal.Length == 1 ? (splitDecimal[0], null) : (splitDecimal[0], splitDecimal[1]);
    }

    public static ushort CastToUshort(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;

        var isParsed = ushort.TryParse(value, out var parsedValue);

        if (isParsed) return parsedValue;

        var message = string.Format(Message.AnErrorOccuredWhileCasting, typeof(string), typeof(ushort));
        throw new InvalidCastException(message);
    }
    
    public static int CastToInt(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;

        var isParsed = int.TryParse(value, out var parsedValue);

        if (isParsed) return parsedValue;

        var message = string.Format(Message.AnErrorOccuredWhileCasting, typeof(string), typeof(int));
        throw new InvalidCastException(message);
    }
}