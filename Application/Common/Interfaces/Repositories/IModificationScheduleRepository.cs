using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IModificationScheduleRepository
{
    Task<ModificationSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ModificationSchedule>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ModificationSchedule>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ModificationSchedule>> GetUpcomingSchedulesAsync(DateTime fromDate, CancellationToken cancellationToken = default);
    Task AddAsync(ModificationSchedule schedule, CancellationToken cancellationToken = default);
    Task UpdateAsync(ModificationSchedule schedule, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    IUnitOfWork UnitOfWork { get; }
}