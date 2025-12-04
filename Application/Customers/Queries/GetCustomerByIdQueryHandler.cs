using Application.Common;
using Application.Common.Customers.Queries;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<Customer>>
{
    private readonly ICustomerQueries _queries;

    public GetCustomerByIdQueryHandler(ICustomerQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _queries.GetByIdAsync(request.Id, cancellationToken);

        if (customer == null)
        {
            return Result<Customer>.Failure(Error.NotFound("Customer.NotFound", $"Customer with ID {request.Id} not found."));
        }

        return Result<Customer>.Success(customer);
    }
}