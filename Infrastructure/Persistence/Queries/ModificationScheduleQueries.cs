using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class ModificationScheduleQueries : IModificationScheduleQueries
{
    private readonly ApplicationDbContext _context;

    public ModificationScheduleQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ModificationSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(ms => ms.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ModificationSchedule>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ModificationSchedule>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        return await _context.ModificationSchedules
            .AsNoTracking()
            .Where(ms => ms.CarId == carId)
            .ToListAsync(cancellationToken);
    }
}