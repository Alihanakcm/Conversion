using System.Runtime.InteropServices;
using Business.Managers;
using Business.Managers.Abstracts;
using Core.Constants;
using Core.Extensions;
using Microsoft.Win32.SafeHandles;

namespace Business.Converters;

public sealed class EnglishConverter : IConverter
{
    private readonly IConversionManager _conversionManager;
    private bool _disposedValue;
    private readonly SafeHandle _safeHandle = new SafeFileHandle(nint.Zero, true);

    public EnglishConverter(IConversionManager conversionManager)
    {
        _conversionManager = conversionManager;
        Dispose(false);
    }

    public string Convert(string numberPart)
    {
        if (string.IsNullOrWhiteSpace(numberPart))
            return string.Empty;

        var blockList = DigitManager.SeparateToBlocks(numberPart);

        for (var i = 0; i < blockList.Count; i++)
        {
            var block = blockList[i];
            if (block.Length == 1 || block is [_, Constant.Zero])
            {
                var unitAsString = DigitManager.GetDigitAsString(block[0].ToString().CastToUshort());
                var digitValue = DigitManager.GetDigitValueAsString(i + 1);
                _conversionManager.Add(unitAsString, digitValue);
                continue;
            }

            string tenfold;
            if (block[1] == Constant.One)
            {
                var tens = string.Concat(block[1], block[0]);
                tenfold = DigitManager.GetDigitAsString(tens.CastToUshort());
            }
            else
            {
                var units = block[0].ToString().CastToUshort();
                var tens = block[1].ToString().CastToUshort() * 10;
                var unitsAsString = units > 0 ? DigitManager.GetDigitAsString(units) : string.Empty;
                var tensAsString = tens > 0 ? DigitManager.GetDigitAsString(tens) : string.Empty;

                var separator = !string.IsNullOrWhiteSpace(tensAsString) && !string.IsNullOrWhiteSpace(unitsAsString)
                    ? Constant.Dash
                    : Constant.Space;

                tenfold = !string.IsNullOrWhiteSpace(tensAsString) && !string.IsNullOrWhiteSpace(unitsAsString)
                    ? string.Join(separator, tensAsString, unitsAsString)
                    : string.Concat(tensAsString, unitsAsString);
            }

            if (block.Length < 3)
            {
                var digitValue = DigitManager.GetDigitValueAsString(i + 1);
                _conversionManager.Add(tenfold, digitValue);
                continue;
            }

            var hundreds = block[2].ToString().CastToUshort() * 100;
            var hundredsAsString = hundreds > 0 ? DigitManager.GetDigitAsString(hundreds) : string.Empty;

            var digit = !string.IsNullOrWhiteSpace(tenfold)
                ? string.Join(Constant.Space, hundredsAsString, tenfold)
                : hundredsAsString;
            _conversionManager.Add(digit, DigitManager.GetDigitValueAsString(i + 1));
        }

        var conversionResult = _conversionManager.Get();
        _conversionManager.Clear();

        return conversionResult;
    }

    public void Dispose() => Dispose(true);

    private void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing)
        {
            _safeHandle.Dispose();
        }

        _disposedValue = true;
    }
}