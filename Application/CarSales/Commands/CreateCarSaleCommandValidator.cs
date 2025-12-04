using FluentValidation;

namespace Application.Common.CarSales.Commands;

public class CreateCarSaleCommandValidator : AbstractValidator<CreateCarSaleCommand>
{
    public CreateCarSaleCommandValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("CarId is required");

        RuleFor(x => x.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required")
            .MaximumLength(50).WithMessage("Sale number must not exceed 50 characters");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required")
            .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters");

        RuleFor(x => x.CustomerContact)
            .NotEmpty().WithMessage("Customer contact is required")
            .MaximumLength(200).WithMessage("Customer contact must not exceed 200 characters");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Invalid sale priority");

        RuleFor(x => x.ScheduledDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Scheduled date cannot be in the past");
    }
}