using Domain.Enums;
using MediatR;

namespace Application.Common.CarSales.Commands;

public record CreateCarSaleCommand(
    Guid CarId,
    Guid? CustomerId,
    string SaleNumber,
    string CustomerName,
    string CustomerContact,
    string Title,
    string Description,
    SalePriority Priority,
    DateTime ScheduledDate
) : IRequest<Result<Guid>>;