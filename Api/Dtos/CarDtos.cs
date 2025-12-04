using Domain.Enums;

namespace Api.Dtos;

public record CreateCarRequest(
    string Make,
    string Model,
    string VinNumber,
    string Color,
    decimal Price,
    DateTime Year
);

public record UpdateCarRequest(
    string Make,
    string Model,
    string Color,
    decimal Price
);

public record UpdateCarStatusRequest(
    CarStatus Status
);

public record CarResponse(
    Guid Id,
    string Make,
    string Model,
    string VinNumber,
    string Color,
    decimal Price,
    CarStatus Status,
    DateTime Year,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);