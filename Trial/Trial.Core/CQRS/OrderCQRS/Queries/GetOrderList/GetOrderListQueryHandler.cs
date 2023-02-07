using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Order;


namespace Trial.Core.CQRS.OrderCQRS.Queries.GetOrderList;


public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderDTO>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetOrderListQueryHandler(ICustomerRepository customerRepository, 
        IOrderRepository orderRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<List<OrderDTO>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var filter = request.OrderFilter;
        IEnumerable<Order> orderList;
        List<OrderDTO> orderDTOList;

        if (filter == null)
        {
            orderList = (await orderRepository.GetAllAsync()).ToList();
        }
        else
        {
            var customer = await customerRepository.GetByIdAsync(filter.CustomerId);

            if (customer == null)
                throw new NotFoundException(nameof(Customer), filter.CustomerId);

            orderList = await orderRepository.GetAllAsync();
            orderList = orderList.Where(x => x.CustomerId == customer.Id);
            orderList = orderList.Where(x => x.CreatedAt >= filter.From && x.CreatedAt <= filter.To);
            orderList = orderList.OrderBy(x => x.CreatedAt).ToList();
        }

        orderDTOList = mapper.Map<List<OrderDTO>>(orderList);

        return orderDTOList;
    }
}
