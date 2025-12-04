using Domain.Entities;
using MediatR;

namespace Application.Common.CarSales.Queries;

public record GetCarSaleByIdQuery(Guid Id) : IRequest<Result<CarSale>>;