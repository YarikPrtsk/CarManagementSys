using Application.Common;
using Application.Common.Customers.Commands;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Customers.Commands;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
{
    private readonly ICustomerRepository _repository;

    public UpdateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(Error.NotFound("Customer.NotFound", $"Customer with ID {request.Id} not found."));
        }

        customer.UpdateDetails(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Address
        );

        await _repository.UpdateAsync(customer, cancellationToken);

        return Result.Success();
    }
}