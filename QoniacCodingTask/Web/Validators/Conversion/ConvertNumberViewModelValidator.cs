using Core.Constants.Message;
using FluentValidation;
using Web.Models.Conversion;

namespace Web.Validators.Conversion;

public class ConvertNumberViewModelValidator : AbstractValidator<ConvertNumberViewModel>
{
    public ConvertNumberViewModelValidator()
    {
        RuleFor(model => model.SelectedCurrency).GreaterThan(0).WithMessage(Message.TheFieldIsRequired);

        RuleFor(model => model.SelectedLanguage).GreaterThan(0).WithMessage(Message.TheFieldIsRequired);

        RuleFor(model => model.Number).NotEmpty().WithMessage(Message.TheFieldIsRequired);
    }
}