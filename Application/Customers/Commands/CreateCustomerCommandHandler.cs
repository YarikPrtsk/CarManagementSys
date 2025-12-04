using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Common.Customers.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid>>
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.New(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Address,
            request.DateOfBirth
        );

        await _repository.AddAsync(customer, cancellationToken);

        return Result<Guid>.Success(customer.Id);
    }
}