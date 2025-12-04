using Domain.Entities;
using MediatR;

namespace Application.Common.ModificationSchedules.Queries;

public record GetModificationSchedulesByCarIdQuery(Guid CarId)
    : IRequest<Result<IEnumerable<ModificationSchedule>>>;