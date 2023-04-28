using Business.Converters;
using Business.Managers;
using Business.Managers.Abstracts;
using Business.Services.ConversionService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Business;

public static class Startup
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IConverterFactory, ConverterFactory>();
        services.AddScoped<IConversionService, ConversionService>();
        services.AddScoped<EnglishConverter>();
        services.AddScoped<IConversionManager, ConversionManager>();
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();
        services.AddSingleton(typeof(ILogger), logger);
    }
}

public class ApplicationLogs
{
}