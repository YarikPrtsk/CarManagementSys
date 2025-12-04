using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface ICustomerQueries
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
}