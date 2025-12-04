using FluentValidation;

namespace Application.Common.CarSales.Commands;

public class StartCarSaleCommandValidator : AbstractValidator<StartCarSaleCommand>
{
    public StartCarSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}