using MediatR;
using Trial.Core.DTO.Order;


namespace Trial.Core.CQRS.OrderCQRS.Commands.CreateOrderCommand;


public class CreateOrderCommand : IRequest<OrderDTO>
{
    public CreateOrderDTO CreateOrderDTO { get; set; }
}
