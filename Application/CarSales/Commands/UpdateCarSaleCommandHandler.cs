using Application.Common;
using Application.Common.CarSales.Commands;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.CarSales.Commands;

public class UpdateCarSaleCommandHandler : IRequestHandler<UpdateCarSaleCommand, Result>
{
    private readonly ICarSaleRepository _repository;

    public UpdateCarSaleCommandHandler(ICarSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateCarSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            return Result.Failure(Error.NotFound("CarSale.NotFound", $"CarSale with ID {request.Id} not found."));
        }

        sale.UpdateDetails(request.Title, request.Description, request.Priority, request.ScheduledDate);

        await _repository.UpdateAsync(sale, cancellationToken);

        return Result.Success();
    }
}