using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Common.Customers.Commands;

public record UpdateCustomerStatusCommand(
    Guid Id,
    CustomerStatus Status
) : IRequest<Result>;