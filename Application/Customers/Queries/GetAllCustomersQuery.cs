using Domain.Entities;
using MediatR;

namespace Application.Common.Customers.Queries;

public record GetAllCustomersQuery : IRequest<Result<IEnumerable<Customer>>>;