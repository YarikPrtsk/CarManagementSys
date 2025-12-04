using Domain.Enums;

namespace Api.Dtos;

public record CreateCarSaleRequest(
    Guid CarId,
    Guid? CustomerId,
    string SaleNumber,
    string CustomerName,
    string CustomerContact,
    string Title,
    string Description,
    SalePriority Priority,
    DateTime ScheduledDate
);

public record UpdateCarSaleRequest(
    string Title,
    string Description,
    SalePriority Priority,
    DateTime ScheduledDate
);

public record CompleteCarSaleRequest(
    string CompletionNotes
);

public record CarSaleResponse(
    Guid Id,
    string SaleNumber,
    Guid CarId,
    Guid? CustomerId,
    string CustomerName,
    string CustomerContact,
    string Title,
    string Description,
    SalePriority Priority,
    SaleStatus Status,
    DateTime ScheduledDate,
    DateTime? CompletedAt,
    string? CompletionNotes,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);