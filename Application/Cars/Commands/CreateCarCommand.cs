using MediatR;

namespace Application.Common.Cars.Commands;

public record CreateCarCommand(
    string Make,
    string Model,
    string VinNumber,
    string Color,
    decimal Price,
    DateTime Year
) : IRequest<Result<Guid>>;