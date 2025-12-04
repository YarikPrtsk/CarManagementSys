using Domain.Enums;
using MediatR;

namespace Application.Common.ModificationSchedules.Commands;

public record UpdateModificationScheduleCommand(
    Guid Id,
    string TaskName,
    string Description,
    ModificationFrequency Frequency,
    DateTime NextDueDate
) : IRequest<Result>;