using Business.Managers;
using Core.Constants;
using Core.Extensions;

namespace Business.Converters;

public class EnglishConverter : IConverter
{
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
                ConversionManager.Add(unitAsString, digitValue);
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
                ConversionManager.Add(tenfold, digitValue);
                continue;
            }

            var hundreds = block[2].ToString().CastToUshort() * 100;
            var hundredsAsString = hundreds > 0 ? DigitManager.GetDigitAsString(hundreds) : string.Empty;

            var digit = !string.IsNullOrWhiteSpace(tenfold)
                ? string.Join(Constant.Space, hundredsAsString, tenfold)
                : hundredsAsString;
            ConversionManager.Add(digit, DigitManager.GetDigitValueAsString(i + 1));
        }

        var conversionResult = ConversionManager.Get();
        ConversionManager.Clear();

        return conversionResult;
    }
}