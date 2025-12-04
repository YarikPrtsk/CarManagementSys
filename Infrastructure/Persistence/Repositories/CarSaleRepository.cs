using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CarSaleRepository : ICarSaleRepository
{
    private readonly ApplicationDbContext _context;

    public CarSaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CarSale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CarSales
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<CarSale>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.CarSales
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CarSale carSale, CancellationToken cancellationToken = default)
    {
        await _context.CarSales.AddAsync(carSale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CarSale carSale, CancellationToken cancellationToken = default)
    {
        _context.CarSales.Update(carSale);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CarSales
            .AnyAsync(s => s.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<CarSale>> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        return await _context.CarSales
            .Where(s => s.CarId == carId)
            .ToListAsync(cancellationToken);
    }
}