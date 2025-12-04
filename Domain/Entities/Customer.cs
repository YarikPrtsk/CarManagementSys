using Domain.Enums;

namespace Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public CustomerStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Customer() { }

    public static Customer New(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string address,
        DateTime dateOfBirth)
    {
        return new Customer
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            DateOfBirth = dateOfBirth,
            Status = CustomerStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateDetails(string firstName, string lastName, string email, string phoneNumber, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(CustomerStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
