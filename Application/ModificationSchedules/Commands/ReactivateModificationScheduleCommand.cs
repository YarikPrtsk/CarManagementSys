using MediatR;

namespace Application.Common.ModificationSchedules.Commands;

public record ReactivateModificationScheduleCommand(
    Guid Id,
    DateTime NextDueDate
) : IRequest<Result>;