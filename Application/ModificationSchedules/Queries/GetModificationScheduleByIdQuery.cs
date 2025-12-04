using Domain.Entities;
using MediatR;

namespace Application.Common.ModificationSchedules.Queries;

public record GetModificationScheduleByIdQuery(Guid Id)
    : IRequest<Result<ModificationSchedule>>;