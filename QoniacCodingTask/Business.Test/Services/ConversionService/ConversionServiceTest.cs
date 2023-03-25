using Business.Converters;
using Business.Services.ConversionService;
using Business.Services.ConversionService.Dto;
using Core.Constants.Enum;
using Microsoft.Extensions.Logging;
using Moq;

namespace Business.Test.Services.ConversionService;

public class ConversionServiceTest
{
    [Theory]
    [InlineData(0, Currency.Dollar, ConverterType.English, "zero dollars")]
    [InlineData(1, Currency.Dollar, ConverterType.English, "one dollar")]
    [InlineData(25.1, Currency.Dollar, ConverterType.English, "twenty-five dollars and ten cents")]
    [InlineData(0.01, Currency.Dollar, ConverterType.English, "zero dollars and one cent")]
    [InlineData(45100, Currency.Dollar, ConverterType.English, "forty-five thousand one hundred dollars")]
    [InlineData(999999999.99, Currency.Dollar, ConverterType.English,
        "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
    public void Send_Decimal_Number_Returns_Conversion(decimal testNumber, Currency testCurrency,
        ConverterType testConverterType, string expected)
    {
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock
            .Setup(x => x.GetService(typeof(EnglishConverter)))
            .Returns(serviceProviderMock.Object);

        var converterFactoryMock = new Mock<IConverterFactory>();
        converterFactoryMock.Setup(x => x.CreateConverter(ConverterType.English)).Returns(new EnglishConverter());

        var request = new ConvertRequestDto
        {
            ConverterType = testConverterType,
            Currency = testCurrency,
            Number = testNumber
        };
        
        var englishConverterMock = new Mock<IConversionService>();
        englishConverterMock.Setup(x => x.Convert(request)).Returns(expected);

        englishConverterMock.Object.Convert(request);

        var loggerMock = new Mock<ILogger>();
        var conversionService = new Business.Services.ConversionService.ConversionService(converterFactoryMock.Object, loggerMock.Object);
        var actual = conversionService.Convert(request);

        Assert.Equal(expected, actual);
    }
}