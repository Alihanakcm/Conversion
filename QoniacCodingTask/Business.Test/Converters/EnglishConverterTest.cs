using Business.Converters;
using Core.Constants.Enum;
using Moq;

namespace Business.Test.Converters;

public class EnglishConverterTest
{
    [Theory]
    [InlineData("0", "zero")]
    [InlineData("1", "one")]
    [InlineData("25", "twenty-five")]
    [InlineData("01", "one")]
    [InlineData("45100", "forty-five thousand one hundred")]
    public void Send_Number_To_English_Conversion_Returns_Conversion_Result(string test, string expected)
    {
        var serviceProvider = new Mock<IServiceProvider>();
        serviceProvider
            .Setup(x => x.GetService(typeof(EnglishConverter)))
            .Returns(serviceProvider.Object);

        var converterFactory = new Mock<IConverterFactory>();
        converterFactory.Setup(x => x.CreateConverter(ConverterType.English)).Returns(new EnglishConverter());

        var englishConverterMock = new Mock<IConverter>();
        englishConverterMock.Setup(x => x.Convert(test)).Returns(expected);

        englishConverterMock.Object.Convert(test);

        var englishConverter = new EnglishConverter();
        var actual = englishConverter.Convert(test);

        Assert.Equal(expected, actual);
    }
}