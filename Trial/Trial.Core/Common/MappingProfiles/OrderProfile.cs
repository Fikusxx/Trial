using AutoMapper;
using Trial.Core.Domain;
using Trial.Core.DTO.Order;


namespace Trial.Core.Common.MappingProfiles;


public class OrderProfile : Profile
{
	public OrderProfile()
	{
		CreateMap<Order, OrderDTO>().ReverseMap();
		CreateMap<Order, CreateOrderDTO>().ReverseMap();
		CreateMap<Order, UpdateOrderDTO>().ReverseMap();

	}
}
