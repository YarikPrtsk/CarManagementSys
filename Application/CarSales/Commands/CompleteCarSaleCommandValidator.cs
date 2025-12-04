using FluentValidation;

namespace Application.Common.CarSales.Commands;

public class CompleteCarSaleCommandValidator : AbstractValidator<CompleteCarSaleCommand>
{
    public CompleteCarSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.CompletionNotes)
            .NotEmpty().WithMessage("Completion notes are required")
            .MaximumLength(1000).WithMessage("Completion notes must not exceed 1000 characters");
    }
}