using Domain.Entities;
using MediatR;

namespace Application.Common.Customers.Queries;

public record GetCustomerByIdQuery(Guid Id) : IRequest<Result<Customer>>;