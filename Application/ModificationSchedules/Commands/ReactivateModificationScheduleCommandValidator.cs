using FluentValidation;

namespace Application.Common.ModificationSchedules.Commands;

public class ReactivateModificationScheduleCommandValidator : AbstractValidator<ReactivateModificationScheduleCommand>
{
    public ReactivateModificationScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.NextDueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Next due date cannot be in the past");
    }
}