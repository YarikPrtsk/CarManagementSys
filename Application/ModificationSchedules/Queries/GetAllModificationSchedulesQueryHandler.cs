using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Common.ModificationSchedules.Queries;

public class GetAllModificationSchedulesQueryHandler : IRequestHandler<GetAllModificationSchedulesQuery, Result<IEnumerable<ModificationSchedule>>>
{
    private readonly IModificationScheduleQueries _queries;

    public GetAllModificationSchedulesQueryHandler(IModificationScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<ModificationSchedule>>> Handle(GetAllModificationSchedulesQuery request, CancellationToken cancellationToken)
    {
        var schedules = await _queries.GetAllAsync(cancellationToken);
        return Result<IEnumerable<ModificationSchedule>>.Success(schedules);
    }
}