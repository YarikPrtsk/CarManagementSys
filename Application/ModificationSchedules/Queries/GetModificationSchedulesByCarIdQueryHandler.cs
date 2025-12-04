using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Common.ModificationSchedules.Queries;

public class GetModificationSchedulesByCarIdQueryHandler : IRequestHandler<GetModificationSchedulesByCarIdQuery, Result<IEnumerable<ModificationSchedule>>>
{
    private readonly IModificationScheduleQueries _queries;

    public GetModificationSchedulesByCarIdQueryHandler(IModificationScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<ModificationSchedule>>> Handle(GetModificationSchedulesByCarIdQuery request, CancellationToken cancellationToken)
    {
        var schedules = await _queries.GetByCarIdAsync(request.CarId, cancellationToken);
        return Result<IEnumerable<ModificationSchedule>>.Success(schedules);
    }
}