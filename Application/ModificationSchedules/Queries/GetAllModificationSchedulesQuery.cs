using Domain.Entities;
using MediatR;

namespace Application.Common.ModificationSchedules.Queries;

public record GetAllModificationSchedulesQuery
    : IRequest<Result<IEnumerable<ModificationSchedule>>>;