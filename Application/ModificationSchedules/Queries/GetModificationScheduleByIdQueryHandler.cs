using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.ModificationSchedules.Queries;
using Domain.Entities;
using MediatR;

namespace Application.ModificationSchedules.Queries;

public class GetModificationScheduleByIdQueryHandler : IRequestHandler<GetModificationScheduleByIdQuery, Result<ModificationSchedule>>
{
    private readonly IModificationScheduleQueries _queries;

    public GetModificationScheduleByIdQueryHandler(IModificationScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<ModificationSchedule>> Handle(GetModificationScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _queries.GetByIdAsync(request.Id, cancellationToken);

        if (schedule == null)
        {
            return Result<ModificationSchedule>.Failure(Error.NotFound("ModificationSchedule.NotFound", $"Schedule with ID {request.Id} not found."));
        }

        return Result<ModificationSchedule>.Success(schedule);
    }
}