using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface ICarSaleQueries
{
    Task<CarSale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CarSale>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<CarSale>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default);
}