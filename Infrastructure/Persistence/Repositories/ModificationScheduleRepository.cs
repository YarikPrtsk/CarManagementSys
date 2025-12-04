using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ModificationScheduleRepository : IModificationScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public ModificationScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<ModificationSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .FirstOrDefaultAsync(ms => ms.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ModificationSchedule>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<ModificationSchedule>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .Where(ms => ms.CarId == carId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<ModificationSchedule>> GetUpcomingSchedulesAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .Where(ms => ms.IsActive && ms.NextDueDate <= date)
            .OrderBy(ms => ms.NextDueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ModificationSchedule modificationSchedule, CancellationToken cancellationToken = default)
    {
        await _context.ModificationSchedules.AddAsync(modificationSchedule, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ModificationSchedule modificationSchedule, CancellationToken cancellationToken = default)
    {
        _context.ModificationSchedules.Update(modificationSchedule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .AnyAsync(ms => ms.Id == id, cancellationToken);
    }
}
