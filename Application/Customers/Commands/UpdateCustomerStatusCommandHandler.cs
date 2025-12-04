using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Common.Customers.Commands;

public class UpdateCustomerStatusCommandHandler : IRequestHandler<UpdateCustomerStatusCommand, Result>
{
    private readonly ICustomerRepository _repository;

    public UpdateCustomerStatusCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateCustomerStatusCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(Error.NotFound("Customer.NotFound", $"Customer with ID {request.Id} not found."));
        }

        customer.ChangeStatus(request.Status);

        await _repository.UpdateAsync(customer, cancellationToken);

        return Result.Success();
    }
}