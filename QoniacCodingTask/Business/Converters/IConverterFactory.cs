using Core.Constants.Enum;

namespace Business.Converters;

public interface IConverterFactory
{
    IConverter? CreateConverter(ConverterType converterType);
}