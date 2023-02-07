using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Order;

namespace Trial.Core.CQRS.OrderCQRS.Commands.CreateOrderCommand;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public CreateOrderCommandHandler(ICustomerRepository customerRepository, 
        IOrderRepository orderRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<OrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CreateOrderDTO.CustomerId);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.CreateOrderDTO.CustomerId);

        var order = mapper.Map<Order>(request.CreateOrderDTO);
        await orderRepository.AddAsync(order);
        await orderRepository.SaveAsync();
        var orderDTO = mapper.Map<OrderDTO>(order);

        return orderDTO;
    }
}
