using FluentValidation;

namespace Application.Common.CarSales.Commands;

public class CancelCarSaleCommandValidator : AbstractValidator<CancelCarSaleCommand>
{
    public CancelCarSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}