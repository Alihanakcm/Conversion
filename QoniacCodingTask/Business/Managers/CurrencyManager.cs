using System.ComponentModel;
using Core.Constants;
using Core.Constants.Enum;

namespace Business.Managers;

public static class CurrencyManager
{
    public static (string, string) GetCurrencyAsString(Currency currency, int integerPart, int decimalPart)
    {
        string intCurrency, decimalCurrency;
        switch (currency)
        {
            case Currency.Dollar:
            {
                intCurrency = integerPart != 1 ? Constant.Dollars : Constant.Dollar;
                decimalCurrency = decimalPart != 1 ? Constant.Cents : Constant.Cent;
                break;
            }
            case Currency.Euro:
            {
                intCurrency = integerPart != 1 ? Constant.Euros : Constant.Euro;
                decimalCurrency = decimalPart != 1 ? Constant.Cents : Constant.Cent;
                break;
            }
            case Currency.Pound:
            {
                intCurrency = integerPart != 1 ? Constant.Pounds : Constant.Pound;
                decimalCurrency = decimalPart != 1 ? Constant.Pennies : Constant.Penny;
                break;
            }
            default:
                throw new InvalidEnumArgumentException();
        }

        return (intCurrency, decimalCurrency);
    }
}