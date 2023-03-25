using Business.Services.ConversionService;
using Core.Constants;
using Core.Constants.Message;
using Microsoft.AspNetCore.Mvc;
using Web.Mapping;
using Web.ModelCreators.SelectListModelCreator;
using Web.Models.Conversion;

namespace Web.Controllers;

[Route("")]
public class ConversionController : Controller
{
    private readonly IConversionService _conversionService;

    public ConversionController(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpGet]
    public IActionResult Index(ConvertNumberViewModel viewModel)
    {
        PrepareViewModel(viewModel);
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Convert(ConvertNumberViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            PrepareViewModel(viewModel);
            return View("Index", viewModel);
        }

        viewModel.Number = viewModel.Number.Replace(",", ".");
      
        var isParsed = decimal.TryParse(viewModel.Number, out var parsedNumber);
        if (!isParsed || parsedNumber < 0)
        {
            ModelState.AddModelError(Constant.Number, Message.InvalidEnteredNumber);
            PrepareViewModel(viewModel);
            return View("Index", viewModel);
        }

        var requestDto = viewModel.ToRequestDto(parsedNumber);
        viewModel.ConversionResult = _conversionService.Convert(requestDto);

        return RedirectToAction("Index", viewModel);
    }

    private void PrepareViewModel(ConvertNumberViewModel viewModel)
    {
        viewModel.Currencies = SelectListModelCreator.GetCurrencySelectList();
        viewModel.Languages = SelectListModelCreator.GetLanguageSelectList();
    }
}