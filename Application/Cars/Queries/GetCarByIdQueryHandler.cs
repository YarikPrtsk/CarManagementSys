using Application.Common;
using Application.Common.Cars.Queries;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Cars.Queries;

public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Result<Car>>
{
    private readonly ICarQueries _queries;

    public GetCarByIdQueryHandler(ICarQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<Car>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var car = await _queries.GetByIdAsync(request.Id, cancellationToken);
        
        if (car == null)
        {
            return Result<Car>.Failure(Error.NotFound("Car.NotFound", $"Car with ID {request.Id} not found."));
        }

        return Result<Car>.Success(car);
    }
}