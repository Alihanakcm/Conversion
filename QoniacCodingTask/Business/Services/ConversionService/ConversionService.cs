using Business.Converters;
using Business.Managers;
using Business.Services.ConversionService.Dto;
using Core.Constants;
using Core.Constants.Enum;
using Core.Constants.Message;
using Core.Extensions;
using Microsoft.Extensions.Logging;

namespace Business.Services.ConversionService;

public class ConversionService : IConversionService
{
    private readonly IConverterFactory _converterFactory;
    private readonly ILogger _logger;

    public ConversionService(
        IConverterFactory converterFactory,
        ILogger logger)
    {
        _converterFactory = converterFactory;
        _logger = logger;
    }

    public string Convert(ConvertRequestDto requestDto)
    {
        var converter = _converterFactory.CreateConverter(requestDto.ConverterType);
        if (converter == null)
        {
            _logger.LogError(
                $"An error occured while creating a converter. Converter type => {requestDto.ConverterType}");
            throw new Exception(Message.AnErrorOccured);
        }

        var (integerPartAsString, decimalPartAsString) = SplitDecimal(requestDto.Number);

        var convertedIntegerPart = converter.Convert(integerPartAsString);
        var converterDecimalPart =
            !string.IsNullOrWhiteSpace(decimalPartAsString) ? converter.Convert(decimalPartAsString) : string.Empty;

        converter.Dispose();
        var (integerPartCurrency, decimalPartCurrency) =
            GetCurrency(requestDto.Currency, integerPartAsString, decimalPartAsString);

        var conversionResult = string.Join(Constant.Space, convertedIntegerPart, integerPartCurrency);
        if (!string.IsNullOrWhiteSpace(converterDecimalPart))
            conversionResult += Constant.AndWithSpaces +
                                string.Join(Constant.Space, converterDecimalPart, decimalPartCurrency);

        return conversionResult;
    }

    private (string integerPart, string? decimalPart) SplitDecimal(decimal number)
    {
        var (integerPart, decimalPart) = number.Split();

        if (string.IsNullOrWhiteSpace(decimalPart)) return (integerPart, decimalPart);

        var decimalPartAsUshort = decimalPart.CastToUshort();
        if (decimalPartAsUshort > 0 && decimalPart.Length == 1)
        {
            decimalPartAsUshort *= 10;
            decimalPart = decimalPartAsUshort.ToString();
        }

        return (integerPart, decimalPart);
    }

    private (string, string) GetCurrency(Currency currency, string integerPartAsString, string? decimalPartAsString)
    {
        var integerPart = integerPartAsString.CastToInt();
        var decimalPart = decimalPartAsString.CastToUshort();
        var (intCurrency, decimalCurrency) =
            CurrencyManager.GetCurrencyAsString(currency, integerPart, decimalPart);

        return (intCurrency, decimalCurrency);
    }
}