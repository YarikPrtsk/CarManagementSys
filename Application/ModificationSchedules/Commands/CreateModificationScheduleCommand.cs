using Domain.Enums;
using MediatR;

namespace Application.Common.ModificationSchedules.Commands;

public record CreateModificationScheduleCommand(
    Guid CarId,
    string TaskName,
    string Description,
    ModificationFrequency Frequency,
    DateTime NextDueDate
) : IRequest<Result<Guid>>;