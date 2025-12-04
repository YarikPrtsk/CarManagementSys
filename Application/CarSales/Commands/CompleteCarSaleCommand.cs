using MediatR;

namespace Application.Common.CarSales.Commands;

public record CompleteCarSaleCommand(
    Guid Id,
    string CompletionNotes
) : IRequest<Result>;