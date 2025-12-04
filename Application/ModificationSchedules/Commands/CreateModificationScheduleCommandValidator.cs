using FluentValidation;

namespace Application.Common.ModificationSchedules.Commands;

public class CreateModificationScheduleCommandValidator : AbstractValidator<CreateModificationScheduleCommand>
{
    public CreateModificationScheduleCommandValidator()
    {
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("Car ID is required");

        RuleFor(x => x.TaskName)
            .NotEmpty().WithMessage("Task name is required")
            .MaximumLength(200).WithMessage("Task name must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Frequency)
            .IsInEnum().WithMessage("Invalid modification frequency");

        RuleFor(x => x.NextDueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Next due date cannot be in the past");
    }
}