using System.ComponentModel;
using Business.Services.ConversionService.Dto;
using Core.Constants.Enum;
using Core.Constants.Message;
using Web.Models.Conversion;

namespace Web.Mapping;

public static class ConversionMapper
{
    public static ConvertRequestDto ToRequestDto(this ConvertNumberViewModel viewModel, decimal number)
    {
        var dto = new ConvertRequestDto
        {
            Currency = viewModel.SelectedCurrency.ToCurrency(),
            ConverterType = viewModel.SelectedLanguage.ToConverterType(),
            Number = number
        };

        return dto;
    }

    private static Currency ToCurrency(this int selectedCurrency)
    {
        return selectedCurrency switch
        {
            1 => Currency.Dollar,
            2 => Currency.Euro,
            3 => Currency.Pound,
            _ => throw new InvalidEnumArgumentException(Message.InvalidCurrency)
        };
    }

    private static ConverterType ToConverterType(this int selectedLanguage)
    {
        return selectedLanguage switch
        {
            1 => ConverterType.English,
            _ => throw new InvalidEnumArgumentException(Message.InvalidLanguage)
        };
    }
}