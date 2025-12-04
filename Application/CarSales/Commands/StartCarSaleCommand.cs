using MediatR;

namespace Application.Common.CarSales.Commands;

public record StartCarSaleCommand(Guid Id) : IRequest<Result>;