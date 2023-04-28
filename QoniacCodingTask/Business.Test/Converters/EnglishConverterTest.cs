using Business.Converters;
using Business.Managers;

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
        var englishConverter = new EnglishConverter(new ConversionManager());
        var actual = englishConverter.Convert(test);

        Assert.Equal(expected, actual);
    }
}