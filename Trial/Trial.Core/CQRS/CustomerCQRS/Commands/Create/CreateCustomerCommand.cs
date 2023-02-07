using MediatR;
using Trial.Core.DTO.Customer;

namespace Trial.Core.CQRS.CustomerCQRS.Commands.Create;

public class CreateCustomerCommand : IRequest<CustomerDTO>
{
    public CreateCustomerDTO CreateCustomerDTO { get; set; }
}
