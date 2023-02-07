using MediatR;
using Trial.Core.Common.Filtering;
using Trial.Core.DTO.OrderDetails;


namespace Trial.Core.CQRS.OrderDetailsCQRS.Queries.GetOrderDetailsList;


public class GetOrderDetailsListQuery : IRequest<List<OrderDetailsDTO>>
{
    public OrderDetailsFilter OrderDetailsFilter { get; set; }
}
