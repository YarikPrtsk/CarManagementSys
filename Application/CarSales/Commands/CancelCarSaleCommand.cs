using MediatR;

namespace Application.Common.CarSales.Commands;

public record CancelCarSaleCommand(Guid Id) : IRequest<Result>;