using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.OrderCQRS.Commands.DeleteOrderCommand;


public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id);

        if (order == null)
            throw new NotFoundException(nameof(Order), request.Id);

        orderRepository.Delete(order);
        await orderRepository.SaveAsync();

        return Unit.Value;
    }
}
