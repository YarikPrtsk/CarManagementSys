using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Common.Cars.Queries;

public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, Result<IEnumerable<Car>>>
{
    private readonly ICarQueries _carQueries;

    public GetAllCarsQueryHandler(ICarQueries carQueries)
    {
        _carQueries = carQueries;
    }

    public async Task<Result<IEnumerable<Car>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await _carQueries.GetAllAsync(cancellationToken);

        return Result<IEnumerable<Car>>.Success(cars); 
    }
}