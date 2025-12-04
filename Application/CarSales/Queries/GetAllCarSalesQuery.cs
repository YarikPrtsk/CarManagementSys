using Domain.Entities;
using MediatR;

namespace Application.Common.CarSales.Queries;

public record GetAllCarSalesQuery : IRequest<Result<IEnumerable<CarSale>>>;