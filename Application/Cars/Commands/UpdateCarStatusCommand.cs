using Domain.Enums;
using MediatR;

namespace Application.Common.Cars.Commands;

public record UpdateCarStatusCommand(
    Guid Id,
    CarStatus Status
) : IRequest<Result>;