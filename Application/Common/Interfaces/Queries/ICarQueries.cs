using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface ICarQueries
{
    Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken = default);
}