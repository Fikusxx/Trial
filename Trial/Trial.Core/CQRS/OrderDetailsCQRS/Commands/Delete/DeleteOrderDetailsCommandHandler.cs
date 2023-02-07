using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.OrderDetailsCQRS.Commands.Delete;


public class DeleteOrderDetailsCommandHandler : IRequestHandler<DeleteOrderDetailsCommand>
{
    private readonly IOrderDetailsRepository orderDetailsRepository;

	public DeleteOrderDetailsCommandHandler(IOrderDetailsRepository orderDetailsRepository)
	{
		this.orderDetailsRepository = orderDetailsRepository;
	}

    public async Task<Unit> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
    {
        var orderDetails = await orderDetailsRepository.GetByIdAsync(request.Id, false);

        if (orderDetails == null)
            throw new NotFoundException(nameof(OrderDetails), request.Id);

        orderDetailsRepository.Delete(orderDetails);
        await orderDetailsRepository.SaveAsync();

        return Unit.Value;
    }
}
