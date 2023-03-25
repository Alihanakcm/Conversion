using System.Text;
using Core.Constants;

namespace Business.Managers;

public static class ConversionManager
{
    private static readonly StringBuilder ConversionResult = new();

    public static string Get()
    {
        return ConversionResult.ToString();
    }

    public static void Clear()
    {
        ConversionResult.Clear();
    }

    public static void Add(string digit, string digitValue)
    {
        if (ConversionResult.Length > 0) AddSpace();
        var blockAsString = string.IsNullOrWhiteSpace(digitValue)
            ? digit
            : string.Join(Constant.Space, digit, digitValue);

        ConversionResult.Insert(0, blockAsString);
    }

    private static void AddSpace()
    {
        ConversionResult.Insert(0, Constant.Space);
    }
}