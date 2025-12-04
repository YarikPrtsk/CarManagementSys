using Domain.Entities;
using Domain.Enums;

namespace Api.Dtos;

public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address,
    DateTime DateOfBirth
);

public record UpdateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address
);

public record UpdateCustomerStatusRequest(
    CustomerStatus Status
);

public record CustomerResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address,
    DateTime DateOfBirth,
    CustomerStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);