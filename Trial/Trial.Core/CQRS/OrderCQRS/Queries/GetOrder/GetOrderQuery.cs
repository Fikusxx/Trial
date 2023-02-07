using MediatR;
using Trial.Core.DTO.Order;

namespace Trial.Core.CQRS.OrderCQRS.Queries.GetOrder;

public class GetOrderQuery : IRequest<OrderDTO>
{
    public int Id { get; set; }
}
