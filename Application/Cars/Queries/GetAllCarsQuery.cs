using Domain.Entities;
using MediatR;

namespace Application.Common.Cars.Queries;

public record GetAllCarsQuery : IRequest<Result<IEnumerable<Car>>>;