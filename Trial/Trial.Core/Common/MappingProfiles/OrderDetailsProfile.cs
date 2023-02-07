using AutoMapper;
using Trial.Core.Domain;
using Trial.Core.DTO.OrderDetails;

namespace Trial.Core.Common.MappingProfiles;


public class OrderDetailsProfile : Profile
{
	public OrderDetailsProfile()
	{
		CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
		CreateMap<OrderDetails, CreateOrderDetailsDTO>().ReverseMap();
		CreateMap<OrderDetails, UpdateOrderDetailsDTO>().ReverseMap();
	}
}
