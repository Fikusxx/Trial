using MediatR;
using Trial.Core.DTO.Customer;

namespace Trial.Core.CQRS.CustomerCQRS.Queries.GetCustomer;

public class GetCustomerQuery : IRequest<CustomerDTO>
{
    public int Id { get; set; }
}
