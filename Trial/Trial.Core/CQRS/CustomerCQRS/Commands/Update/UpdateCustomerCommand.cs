using MediatR;
using Trial.Core.DTO.Customer;


namespace Trial.Core.CQRS.CustomerCQRS.Commands.Update;


public class UpdateCustomerCommand : IRequest
{
    public UpdateCustomerDTO UpdateCustomerDTO { get; set; }
}
