using Core.Constants.Enum;

namespace Business.Converters;

public interface IConverter
{
    public string Convert(string numberPart);
}