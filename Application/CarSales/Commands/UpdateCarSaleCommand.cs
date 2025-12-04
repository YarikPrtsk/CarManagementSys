using Domain.Enums;
using MediatR;

namespace Application.Common.CarSales.Commands;

public record UpdateCarSaleCommand(
    Guid Id,
    string Title,
    string Description,
    SalePriority Priority,
    DateTime ScheduledDate
) : IRequest<Result>;