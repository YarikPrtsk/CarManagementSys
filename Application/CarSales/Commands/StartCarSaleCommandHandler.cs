using Application.Common;
using Application.Common.CarSales.Commands;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.CarSales.Commands;

public class StartCarSaleCommandHandler : IRequestHandler<StartCarSaleCommand, Result>
{
    private readonly ICarSaleRepository _repository;

    public StartCarSaleCommandHandler(ICarSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(StartCarSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            return Result.Failure(Error.NotFound("CarSale.NotFound", $"CarSale with ID {request.Id} not found."));
        }

        sale.StartSale();

        await _repository.UpdateAsync(sale, cancellationToken);

        return Result.Success();
    }
}