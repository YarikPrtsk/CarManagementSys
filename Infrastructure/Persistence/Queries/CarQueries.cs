using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class CarQueries : ICarQueries
{
    private readonly ApplicationDbContext _context;

    public CarQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Cars
            .AsNoTracking()
            .Include(c => c.ModificationSchedule) 
            .Include(c => c.Sales)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Cars
            .AsNoTracking()
            .Include(c => c.ModificationSchedule)
            .Include(c => c.Sales)
            .ToListAsync(cancellationToken);
    }
}