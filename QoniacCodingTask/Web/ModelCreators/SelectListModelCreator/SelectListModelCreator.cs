using Core.Constants.Enum;
using Web.Models.Common;

namespace Web.ModelCreators.SelectListModelCreator;

public static class SelectListModelCreator
{
    public static List<SelectListItem> GetCurrencySelectList()
    {
        var names = Enum.GetNames(typeof(Currency));
        var currencySelectList = new List<SelectListItem>();

        for (var i = 0; i < names.Length; i++)
        {
            var name = names[i];
            var selectListItem = new SelectListItem
            {
                Name = name,
                Value = i + 1
            };

            currencySelectList.Add(selectListItem);
        }

        return currencySelectList;
    }

    public static List<SelectListItem> GetLanguageSelectList()
    {
        var names = Enum.GetNames(typeof(ConverterType));
        var languageSelectList = new List<SelectListItem>();

        for (var i = 0; i < names.Length; i++)
        {
            var name = names[i];
            var selectListItem =
                new SelectListItem
                {
                    Name = name,
                    Value = i + 1
                };

            languageSelectList.Add(selectListItem);
        }

        return languageSelectList;
    }
}