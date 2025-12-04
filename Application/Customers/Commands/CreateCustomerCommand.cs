using MediatR;

namespace Application.Common.Customers.Commands;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address,
    DateTime DateOfBirth
) : IRequest<Result<Guid>>;