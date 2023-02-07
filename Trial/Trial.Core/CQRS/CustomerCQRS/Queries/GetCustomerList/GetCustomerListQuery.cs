using MediatR;
using Trial.Core.DTO.Customer;


namespace Trial.Core.CQRS.CustomerCQRS.Queries.GetCustomerList;


public class GetCustomerListQuery : IRequest<List<CustomerDTO>>
{
}
