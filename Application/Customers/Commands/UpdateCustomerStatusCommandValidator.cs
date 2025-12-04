using FluentValidation;

namespace Application.Common.Customers.Commands;

public class UpdateCustomerStatusCommandValidator : AbstractValidator<UpdateCustomerStatusCommand>
{
    public UpdateCustomerStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid customer status");
    }
}