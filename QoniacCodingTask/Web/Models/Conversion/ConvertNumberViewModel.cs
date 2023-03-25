using Web.Models.Common;

namespace Web.Models.Conversion;

public record ConvertNumberViewModel
{
    public string Number { get; set; } = string.Empty;
    public string? ConversionResult { get; set; }
    public int SelectedCurrency { get; set; }
    public int SelectedLanguage { get; set; }
    public List<SelectListItem>? Currencies { get; set; }
    public List<SelectListItem>? Languages { get; set; }
}