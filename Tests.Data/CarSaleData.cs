using Domain.Entities;
using Domain.Enums;

namespace Tests.Data;

public static class CarSaleData
{
    public static CarSale StandardSale(Guid carId, Guid? customerId = null) => CarSale.New(
        Guid.NewGuid(),
        "SALE-2024-001",
        carId,
        customerId,
        "John Doe",
        "+1234567890",
        "Sale of Toyota Camry",
        "Standard sale procedure",
        SalePriority.Low,
        DateTime.UtcNow.AddDays(3)
    );

    public static CarSale UrgentSale(Guid carId, Guid? customerId = null) => CarSale.New(
        Guid.NewGuid(),
        "SALE-URGENT-002",
        carId,
        customerId,
        "Jane Smith",
        "+0987654321",
        "Urgent Sale",
        "Customer needs car immediately",
        SalePriority.High,
        DateTime.UtcNow.AddDays(1)
    );
}