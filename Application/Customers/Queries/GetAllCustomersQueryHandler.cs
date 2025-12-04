using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Common.Customers.Queries;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result<IEnumerable<Customer>>>
{
    private readonly ICustomerQueries _queries;

    public GetAllCustomersQueryHandler(ICustomerQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _queries.GetAllAsync(cancellationToken);
        return Result<IEnumerable<Customer>>.Success(customers);
    }
}