using Domain.Entities;
using MediatR;

namespace Application.Common.Cars.Queries;

public record GetCarByIdQuery(Guid Id) : IRequest<Result<Car>>;