using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Common.CarSales.Queries;

public class GetAllCarSalesQueryHandler : IRequestHandler<GetAllCarSalesQuery, Result<IEnumerable<CarSale>>>
{
    private readonly ICarSaleQueries _queries;

    public GetAllCarSalesQueryHandler(ICarSaleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<CarSale>>> Handle(GetAllCarSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _queries.GetAllAsync(cancellationToken);
        return Result<IEnumerable<CarSale>>.Success(sales);
    }
}