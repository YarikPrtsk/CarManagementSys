using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface IModificationScheduleQueries
{
    Task<ModificationSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ModificationSchedule>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ModificationSchedule>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default);
}