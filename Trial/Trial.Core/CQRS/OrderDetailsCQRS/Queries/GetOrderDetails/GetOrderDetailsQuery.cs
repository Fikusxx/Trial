using MediatR;
using Trial.Core.DTO.OrderDetails;


namespace Trial.Core.CQRS.OrderDetailsCQRS.Queries.GetOrderDetails;


public class GetOrderDetailsQuery : IRequest<OrderDetailsDTO>
{
    public int Id { get; set; }
}
