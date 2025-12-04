using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Common.ModificationSchedules.Commands;
using Domain.Entities;
using MediatR;

namespace Application.ModificationSchedules.Commands;

public class CreateModificationScheduleCommandHandler : IRequestHandler<CreateModificationScheduleCommand, Result<Guid>>
{
    private readonly IModificationScheduleRepository _scheduleRepository;
    private readonly ICarRepository _carRepository;

    public CreateModificationScheduleCommandHandler(
        IModificationScheduleRepository scheduleRepository, 
        ICarRepository carRepository)
    {
        _scheduleRepository = scheduleRepository;
        _carRepository = carRepository;
    }

    public async Task<Result<Guid>> Handle(CreateModificationScheduleCommand request, CancellationToken cancellationToken)
    {
        if (!await _carRepository.ExistsAsync(request.CarId, cancellationToken))
        {
            return Result<Guid>.Failure(Error.NotFound("Car.NotFound", $"Car with ID {request.CarId} not found."));
        }

        var schedule = ModificationSchedule.New(
            Guid.NewGuid(),
            request.CarId,
            request.TaskName,
            request.Description,
            request.Frequency,
            request.NextDueDate
        );

        await _scheduleRepository.AddAsync(schedule, cancellationToken);

        return Result<Guid>.Success(schedule.Id);
    }
}