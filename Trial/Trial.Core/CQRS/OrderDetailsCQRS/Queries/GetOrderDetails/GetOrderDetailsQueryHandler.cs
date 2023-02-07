using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.OrderDetails;


namespace Trial.Core.CQRS.OrderDetailsCQRS.Queries.GetOrderDetails;


public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsDTO>
{
    private readonly IOrderDetailsRepository orderDetailsRepository;
	private readonly IMapper mapper;

	public GetOrderDetailsQueryHandler(IOrderDetailsRepository orderDetailsRepository,
		IMapper mapper)
	{
		this.orderDetailsRepository = orderDetailsRepository;
		this.mapper = mapper;
	}

    public async Task<OrderDetailsDTO> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
		var orderDetails = await orderDetailsRepository.GetByIdAsync(request.Id, false);

		if (orderDetails == null)
			throw new NotFoundException(nameof(OrderDetails), request.Id);

		var orderDetailsDTO = mapper.Map<OrderDetailsDTO>(orderDetails);

		return orderDetailsDTO;
    }
}
