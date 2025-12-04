using Domain.Enums;

namespace Api.Dtos;

public record CreateModificationScheduleRequest(
    Guid CarId,
    string TaskName,
    string Description,
    ModificationFrequency Frequency,
    DateTime NextDueDate
);

public record UpdateModificationScheduleRequest(
    string TaskName,
    string Description,
    ModificationFrequency Frequency,
    DateTime NextDueDate
);

public record ReactivateModificationScheduleRequest(
    DateTime NextDueDate
);

public record ModificationScheduleResponse(
    Guid Id,
    Guid CarId,
    string TaskName,
    string Description,
    ModificationFrequency Frequency,
    DateTime NextDueDate,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);