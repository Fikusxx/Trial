using MediatR;
using Trial.Core.DTO.OrderDetails;

namespace Trial.Core.CQRS.OrderDetailsCQRS.Commands.Create;

public class CreateOrderDetailsCommand : IRequest<OrderDetailsDTO>
{
    public CreateOrderDetailsDTO CreateOrderDetailsDTO { get; set; }
}
