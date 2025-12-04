using Application.Common;
using Application.Common.CarSales.Commands;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.CarSales.Commands;

public class CompleteCarSaleCommandHandler : IRequestHandler<CompleteCarSaleCommand, Result>
{
    private readonly ICarSaleRepository _repository;

    public CompleteCarSaleCommandHandler(ICarSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(CompleteCarSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            return Result.Failure(Error.NotFound("CarSale.NotFound", $"CarSale with ID {request.Id} not found."));
        }

        sale.Complete(request.CompletionNotes);

        await _repository.UpdateAsync(sale, cancellationToken);

        return Result.Success();
    }
}