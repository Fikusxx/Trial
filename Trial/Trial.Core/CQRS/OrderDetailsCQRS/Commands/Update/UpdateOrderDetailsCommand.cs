using MediatR;
using Trial.Core.DTO.OrderDetails;

namespace Trial.Core.CQRS.OrderDetailsCQRS.Commands.Update;

public class UpdateOrderDetailsCommand : IRequest
{
    public UpdateOrderDetailsDTO UpdateOrderDetailsDTO { get; set; }
}
