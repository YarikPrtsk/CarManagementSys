using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Common.ModificationSchedules.Commands;
using MediatR;

namespace Application.ModificationSchedules.Commands;

public class UpdateModificationScheduleCommandHandler : IRequestHandler<UpdateModificationScheduleCommand, Result>
{
    private readonly IModificationScheduleRepository _repository;

    public UpdateModificationScheduleCommandHandler(IModificationScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateModificationScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (schedule is null)
        {
            return Result.Failure(Error.NotFound("ModificationSchedule.NotFound", $"Schedule with ID {request.Id} not found."));
        }

        schedule.UpdateSchedule(
            request.TaskName,
            request.Description,
            request.Frequency,
            request.NextDueDate
        );

        await _repository.UpdateAsync(schedule, cancellationToken);

        return Result.Success();
    }
}