using FluentValidation;

namespace Application.Common.ModificationSchedules.Commands;

public class DeactivateModificationScheduleCommandValidator : AbstractValidator<DeactivateModificationScheduleCommand>
{
    public DeactivateModificationScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}