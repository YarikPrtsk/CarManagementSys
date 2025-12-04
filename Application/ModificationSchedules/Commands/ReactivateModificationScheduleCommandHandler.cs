using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Common.ModificationSchedules.Commands;
using MediatR;

namespace Application.ModificationSchedules.Commands;

public class ReactivateModificationScheduleCommandHandler : IRequestHandler<ReactivateModificationScheduleCommand, Result>
{
    private readonly IModificationScheduleRepository _repository;

    public ReactivateModificationScheduleCommandHandler(IModificationScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(ReactivateModificationScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (schedule is null)
        {
            return Result.Failure(Error.NotFound("ModificationSchedule.NotFound", $"Schedule with ID {request.Id} not found."));
        }

        await _repository.UpdateAsync(schedule, cancellationToken);

        return Result.Success();
    }
}