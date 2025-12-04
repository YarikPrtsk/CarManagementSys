using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Common.Cars.Commands;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Result<Guid>>
{
    private readonly ICarRepository _repository;

    public CreateCarCommandHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = Car.New(
            Guid.NewGuid(),
            request.Make,
            request.Model,
            request.VinNumber,
            request.Color,
            request.Price,
            request.Year
        );

        await _repository.AddAsync(car, cancellationToken);

        return Result<Guid>.Success(car.Id);
    }
}