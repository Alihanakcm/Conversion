using System.ComponentModel;
using Core.Constants.Enum;
using Core.Constants.Message;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Converters;

public class ConverterFactory : IConverterFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ConverterFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IConverter? CreateConverter(ConverterType converterType)
    {
        using var serviceScope = _serviceProvider.CreateScope();
        var services = serviceScope.ServiceProvider;
        return converterType switch
        {
            ConverterType.English =>
                services.GetRequiredService<EnglishConverter>() as IConverter,
            _ => throw new InvalidEnumArgumentException(Message.InvalidConverterType)
        };
    }
}