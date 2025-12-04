using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Common.CarSales.Queries;

public class GetCarSalesByCarIdQueryHandler : IRequestHandler<GetCarSalesByCarIdQuery, Result<IEnumerable<CarSale>>>
{
    private readonly ICarSaleQueries _queries;

    public GetCarSalesByCarIdQueryHandler(ICarSaleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<CarSale>>> Handle(GetCarSalesByCarIdQuery request, CancellationToken cancellationToken)
    {
        var sales = await _queries.GetByCarIdAsync(request.CarId, cancellationToken);
        return Result<IEnumerable<CarSale>>.Success(sales);
    }
}