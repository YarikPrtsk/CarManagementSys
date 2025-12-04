using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ICarSaleRepository
{
    Task<CarSale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CarSale>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<CarSale>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default);
    Task AddAsync(CarSale carSale, CancellationToken cancellationToken = default);
    Task UpdateAsync(CarSale carSale, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}