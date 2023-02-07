using AutoMapper;
using MediatR;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.OrderDetails;


namespace Trial.Core.CQRS.OrderDetailsCQRS.Queries.GetOrderDetailsList;


public class GetOrderDetailsListQueryHandler : IRequestHandler<GetOrderDetailsListQuery, List<OrderDetailsDTO>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IOrderDetailsRepository orderDetailsRepository;
    private readonly IMapper mapper;

    public GetOrderDetailsListQueryHandler(IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.orderDetailsRepository = orderDetailsRepository;
        this.mapper = mapper;
    }

    public async Task<List<OrderDetailsDTO>> Handle(GetOrderDetailsListQuery request, CancellationToken cancellationToken)
    {
        var filter = request.OrderDetailsFilter;
        IEnumerable<OrderDetails> orderDetailsList = await orderDetailsRepository.GetAllAsync();
        List<OrderDetailsDTO> orderDetailsDTOList;

        if (filter.OrderId == 0 && filter.ProductId == 0)
        {
            // :)
        }
        else
        {
            if (filter.OrderId > 0)
                orderDetailsList = orderDetailsList.Where(x => x.OrderId == filter.OrderId);

            if (filter.ProductId > 0)
                orderDetailsList = orderDetailsList.Where(x => x.ProductId == filter.ProductId);
        }

        orderDetailsList = orderDetailsList.ToList();
        orderDetailsDTOList = mapper.Map<List<OrderDetailsDTO>>(orderDetailsList);

        return orderDetailsDTOList;
    }
}
