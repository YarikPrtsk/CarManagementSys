using Domain.Enums;

namespace Domain.Entities;

public class Car
{
    public Guid Id { get; private set; }
    public string Make { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public string VinNumber { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public CarStatus Status { get; private set; }
    public DateTime Year { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    public virtual ModificationSchedule? ModificationSchedule { get; private set; }
    private readonly List<CarSale> _sales = new();
    public virtual IReadOnlyCollection<CarSale> Sales => _sales.AsReadOnly();

    private Car() { }

    public static Car New(
        Guid id,
        string make,
        string model,
        string vinNumber,
        string color,
        decimal price,
        DateTime year)
    {
        return new Car
        {
            Id = id,
            Make = make,
            Model = model,
            VinNumber = vinNumber,
            Color = color,
            Price = price,
            Status = CarStatus.Available,
            Year = year,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateDetails(string make, string model, string color, decimal price)
    {
        Make = make;
        Model = model;
        Color = color;
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(CarStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
