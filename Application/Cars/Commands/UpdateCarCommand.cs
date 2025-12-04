using MediatR;

namespace Application.Common.Cars.Commands;

public record UpdateCarCommand(
    Guid Id,
    string Make,
    string Model,
    string Color,
    decimal Price
) : IRequest<Result>;