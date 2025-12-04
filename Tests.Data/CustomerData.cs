using Domain.Entities;
using Domain.Enums;

namespace Tests.Data;

public static class CustomerData
{
    public static Customer JohnDoe(Guid? id = null) => Customer.New(
        id ?? Guid.NewGuid(),
        "John",
        "Doe",
        $"john.doe.{Guid.NewGuid()}@example.com",
        $"+1{new Random().Next(100000000, 999999999)}",
        "123 Main St, New York, NY",
        new DateTime(1990, 1, 1).ToUniversalTime()
    );

    public static Customer JaneSmith(Guid? id = null) => Customer.New(
        id ?? Guid.NewGuid(),
        "Jane",
        "Smith",
        $"jane.smith.{Guid.NewGuid()}@example.com",
        $"+1{new Random().Next(100000000, 999999999)}",
        "456 Elm St, Los Angeles, CA",
        new DateTime(1985, 5, 15).ToUniversalTime()
    );
}