using Application.Common;
using Application.Common.Cars.Commands;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Cars.Commands;

public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Result>
{
    private readonly ICarRepository _repository;

    public UpdateCarCommandHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (car is null)
        {
            return Result.Failure(Error.NotFound("Car.NotFound", $"Car with ID {request.Id} not found."));
        }

        car.UpdateDetails(request.Make, request.Model, request.Color, request.Price);

        await _repository.UpdateAsync(car, cancellationToken);

        return Result.Success();
    }
}