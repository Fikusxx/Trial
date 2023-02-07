using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Order;


namespace Trial.Core.CQRS.OrderCQRS.Queries.GetOrder;


public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDTO>
{
    private IOrderRepository orderRepository;
    private IMapper mapper;

    public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<OrderDTO> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id);

        if (order == null)
            throw new NotFoundException(nameof(Order), request.Id);

        var orderDTO = mapper.Map<OrderDTO>(order); 

        return orderDTO;
    }
}
