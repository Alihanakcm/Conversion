using Business.Services.ConversionService.Dto;

namespace Business.Services.ConversionService;

public interface IConversionService
{
    string? Convert(ConvertRequestDto requestDto);
}