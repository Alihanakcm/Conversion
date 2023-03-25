using Core.Constants.Message;

namespace Business.Managers;

public static class DigitManager
{
    public static string GetDigitAsString(int digit)
    {
        if (digit < 0)
            return string.Empty;
        
        var digitAsString = DigitDictionary.FirstOrDefault(x => x.Key == digit);
        if (digitAsString.Value == null)
            throw new ArgumentException(Message.InvalidDigit);

        return digitAsString.Value;
    }

    public static string GetDigitValueAsString(int blockValue)
    {
        var blockValueAsString = BlockValueDictionary.FirstOrDefault(x => x.Key == blockValue);
        if (blockValueAsString.Value == null)
            throw new ArgumentException(Message.InvalidDigit);

        return blockValueAsString.Value;
    }

    public static List<string> SeparateToBlocks(string value)
    {
        var blockList = new List<string>();
        for (var i = value.Length - 1; i >= 0; i -= 3)
        {
            var units = value[i];
            var tens = i - 1 >= 0 ? value[i - 1] : ' ';
            var hundreds = i - 2 >= 0 ? value[i - 2] : ' ';

            var block = string.Concat(units, tens, hundreds);
            blockList.Add(block.Trim());
        }

        return blockList;
    }

    private static readonly Dictionary<ushort, string> DigitDictionary = new()
    {
        { 0, "zero" },
        { 1, "one" },
        { 2, "two" },
        { 3, "three" },
        { 4, "four" },
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" },
        { 10, "ten" },
        { 11, "eleven" },
        { 12, "twelve" },
        { 13, "thirteen" },
        { 14, "fourteen" },
        { 15, "fifteen" },
        { 16, "sixteen" },
        { 17, "seventeen" },
        { 18, "eighteen" },
        { 19, "nineteen" },
        { 20, "twenty" },
        { 30, "thirty" },
        { 40, "forty" },
        { 50, "fifty" },
        { 60, "sixty" },
        { 70, "seventy" },
        { 80, "eighty" },
        { 90, "ninety" },
        { 100, "one hundred" },
        { 200, "two hundred" },
        { 300, "three hundred" },
        { 400, "four hundred" },
        { 500, "five hundred" },
        { 600, "six hundred" },
        { 700, "seven hundred" },
        { 800, "eight hundred" },
        { 900, "nine hundred" }
    };

    private static readonly Dictionary<ushort, string> BlockValueDictionary = new()
    {
        { 1, string.Empty },
        { 2, "thousand" },
        { 3, "million" },
    };
}