using System.Text;
using Business.Managers.Abstracts;
using Core.Constants;

namespace Business.Managers;

public class ConversionManager : IConversionManager
{
    private readonly StringBuilder _conversionResult = new();

    public string Get()
    {
        return _conversionResult.ToString();
    }

    public void Clear()
    {
        _conversionResult.Clear();
    }

    public void Add(string digit, string digitValue)
    {
        if (_conversionResult.Length > 0) AddSpace();
        var blockAsString = string.IsNullOrWhiteSpace(digitValue)
            ? digit
            : string.Join(Constant.Space, digit, digitValue);

        _conversionResult.Insert(0, blockAsString);
    }

    private void AddSpace()
    {
        _conversionResult.Insert(0, Constant.Space);
    }
}