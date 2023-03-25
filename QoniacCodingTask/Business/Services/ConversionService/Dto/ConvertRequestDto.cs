using Core.Constants.Enum;

namespace Business.Services.ConversionService.Dto;

public class ConvertRequestDto
{
    public decimal Number { get; set; }
    public Currency Currency { get; set; }
    public ConverterType ConverterType { get; set; }
}