using FluentValidation;

namespace Application.Common.Cars.Commands;


public class UpdateCarStatusCommandValidator : AbstractValidator<UpdateCarStatusCommand>
{
    public UpdateCarStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid car status");
    }
}