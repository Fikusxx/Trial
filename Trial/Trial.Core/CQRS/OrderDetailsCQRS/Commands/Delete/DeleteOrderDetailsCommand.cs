using MediatR;


namespace Trial.Core.CQRS.OrderDetailsCQRS.Commands.Delete;


public class DeleteOrderDetailsCommand : IRequest
{
    public int Id { get; set; }
}
