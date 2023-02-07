using AutoMapper;
using Trial.Core.Domain;
using Trial.Core.DTO.Customer;

namespace Trial.Core.Common.MappingProfiles;

public class CustomerProfile : Profile
{
	public CustomerProfile()
	{
		CreateMap<Customer, CustomerDTO>().ReverseMap();
		CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
		CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();
	}
}
