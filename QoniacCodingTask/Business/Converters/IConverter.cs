using Core.Constants.Enum;

namespace Business.Converters;

public interface IConverter : IDisposable
{
    public string Convert(string numberPart);
}