using MediatR;
using Trial.Core.Common.Filtering;
using Trial.Core.DTO.Order;

namespace Trial.Core.CQRS.OrderCQRS.Queries.GetOrderList;

public class GetOrderListQuery : IRequest<List<OrderDTO>>
{
    public OrderFilter OrderFilter { get; set; }
}
