using Application.Common;
using Application.Common.CarSales.Commands;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.CarSales.Commands;

public class CreateCarSaleCommandHandler : IRequestHandler<CreateCarSaleCommand, Result<Guid>>
{
    private readonly ICarSaleRepository _carSaleRepository;
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;

    public CreateCarSaleCommandHandler(
        ICarSaleRepository carSaleRepository, 
        ICarRepository carRepository,
        ICustomerRepository customerRepository)
    {
        _carSaleRepository = carSaleRepository;
        _carRepository = carRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCarSaleCommand request, CancellationToken cancellationToken)
    {
        if (!await _carRepository.ExistsAsync(request.CarId, cancellationToken))
        {
            return Result<Guid>.Failure(Error.NotFound("Car.NotFound", $"Car with ID {request.CarId} not found."));
        }

        if (request.CustomerId.HasValue && !await _customerRepository.ExistsAsync(request.CustomerId.Value, cancellationToken))
        {
            return Result<Guid>.Failure(Error.NotFound("Customer.NotFound", $"Customer with ID {request.CustomerId} not found."));
        }

        var carSale = CarSale.New(
            Guid.NewGuid(),
            request.SaleNumber,
            request.CarId,
            request.CustomerId,
            request.CustomerName,
            request.CustomerContact,
            request.Title,
            request.Description,
            request.Priority,
            request.ScheduledDate
        );

        await _carSaleRepository.AddAsync(carSale, cancellationToken);

        return Result<Guid>.Success(carSale.Id);
    }
}