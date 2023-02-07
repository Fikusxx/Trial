using AutoMapper;
using Trial.Core.Domain;
using Trial.Core.DTO.Product;

namespace Trial.Core.Common.MappingProfiles;


public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDTO>().ReverseMap();
		CreateMap<Product, CreateProductDTO>().ReverseMap();
		CreateMap<Product, UpdateProductDTO>().ReverseMap();
	}
}
