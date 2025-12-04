using MediatR;

namespace Application.Common.Customers.Commands;

public record UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address
) : IRequest<Result>;