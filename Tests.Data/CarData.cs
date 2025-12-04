using Domain.Entities;

namespace Tests.Data;

public static class CarData
{
    public static Car ToyotaCamry(string? vinNumber = null)
    {
        return Car.New(
            Guid.NewGuid(),
            "Toyota",
            "Camry",
            vinNumber ?? GenerateUniqueVin("TOYOTA"),
            "Silver",
            25000m,
            new DateTime(2020, 1, 1)
        );
    }

    public static Car HondaCivic(string? vinNumber = null)
    {
        return Car.New(
            Guid.NewGuid(),
            "Honda",
            "Civic",
            vinNumber ?? GenerateUniqueVin("HONDA"),
            "Blue",
            22000m,
            new DateTime(2019, 5, 15)
        );
    }

    public static Car TeslaModel3(string? vinNumber = null)
    {
        return Car.New(
            Guid.NewGuid(),
            "Tesla",
            "Model 3",
            vinNumber ?? GenerateUniqueVin("TESLA"),
            "Red",
            45000m,
            new DateTime(2021, 3, 10)
        );
    }

    private static string GenerateUniqueVin(string prefix)
    {
        var uniquePart = Guid.NewGuid().ToString("N")[..(17 - prefix.Length)].ToUpperInvariant();
        return $"{prefix}{uniquePart}";
    }
}