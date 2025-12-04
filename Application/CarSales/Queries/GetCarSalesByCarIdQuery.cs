using Domain.Entities;
using MediatR;

namespace Application.Common.CarSales.Queries;

public record GetCarSalesByCarIdQuery(Guid CarId) : IRequest<Result<IEnumerable<CarSale>>>;