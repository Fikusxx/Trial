using MediatR;
using Trial.Core.DTO.Order;

namespace Trial.Core.CQRS.OrderCQRS.Commands.UpdateOrderCommand;


public class UpdateOrderCommand : IRequest
{
    public UpdateOrderDTO UpdateOrderDTO { get; set; }
}
