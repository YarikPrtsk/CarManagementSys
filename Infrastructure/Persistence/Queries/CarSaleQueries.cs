using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class CarSaleQueries : ICarSaleQueries
{
    private readonly ApplicationDbContext _context;

    public CarSaleQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CarSale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CarSales
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<CarSale>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.CarSales
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<CarSale>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        return await _context.CarSales
            .AsNoTracking()
            .Where(s => s.CarId == carId)
            .ToListAsync(cancellationToken);
    }
}