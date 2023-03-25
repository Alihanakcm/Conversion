namespace Core.Constants;

public static class Constant
{
    public static char DecimalPoint = '.';
    public static string Dollars = "dollars";
    public static string Dollar = "dollar";
    public static string Euros = "euros";
    public static string Euro = "euro";
    public static string Pounds = "pounds";
    public static string Pound = "pound";
    public static string Cents = "cents";
    public static string Cent = "cent";
    public static string Pennies = "pennies";
    public static string Penny = "penny";
    public static string Number = "Number";
    public static char Space = ' ';
    public static char One = '1';
    public static char Dash = '-';
    public static string AndWithSpaces = " and ";
    public const char Zero = '0';

    public static class Regex
    {
        public static string DecimalNumberRegex = @"^[0-9]\d{0,8}([.,]\d{1,2})?%?$";
    }
}