using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Common.CarSales.Queries;

public class GetCarSaleByIdQueryHandler : IRequestHandler<GetCarSaleByIdQuery, Result<CarSale>>
{
    private readonly ICarSaleQueries _queries;

    public GetCarSaleByIdQueryHandler(ICarSaleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<CarSale>> Handle(GetCarSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _queries.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
        {
            return Result<CarSale>.Failure(Error.NotFound("CarSale.NotFound", $"CarSale with ID {request.Id} not found."));
        }

        return Result<CarSale>.Success(sale);
    }
}