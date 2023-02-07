using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.OrderCQRS.Commands.UpdateOrderCommand;


public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public UpdateOrderCommandHandler(ICustomerRepository customerRepository,
        IOrderRepository orderRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.UpdateOrderDTO.CustomerId);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.UpdateOrderDTO.CustomerId);

        var order = await orderRepository.GetByIdAsync(request.UpdateOrderDTO.Id, false);

        if (order == null)
            throw new NotFoundException(nameof(Order), request.UpdateOrderDTO.Id);

        order = mapper.Map<Order>(request.UpdateOrderDTO);
        orderRepository.Update(order);
        await orderRepository.SaveAsync();

        return Unit.Value;
    }
}
