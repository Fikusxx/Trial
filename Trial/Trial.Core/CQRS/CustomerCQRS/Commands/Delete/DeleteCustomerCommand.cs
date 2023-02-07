using MediatR;

namespace Trial.Core.CQRS.CustomerCQRS.Commands.Delete;

public class DeleteCustomerCommand : IRequest
{
    public int Id { get; set; } 
}
