using MediatR;

namespace Trial.Core.CQRS.OrderCQRS.Commands.DeleteOrderCommand;

public class DeleteOrderCommand : IRequest
{
    public int Id { get; set; }    
}
