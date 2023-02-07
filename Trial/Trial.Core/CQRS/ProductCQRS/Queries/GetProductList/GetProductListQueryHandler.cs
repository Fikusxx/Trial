using AutoMapper;
using MediatR;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Product;

namespace Trial.Core.CQRS.ProductCQRS.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<ProductDTO>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductListQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<List<ProductDTO>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var filter = request.ProductFilter;
        IEnumerable<Product> productList;
        List<ProductDTO> productDTOList;

        if (filter == null)
        {
            productList = (await productRepository.GetAllAsync()).ToList();
        }
        else
        {
            productList = await productRepository.GetAllAsync();
            productList = productList.Where(x => x.Price >= filter.MinPrice && x.Price <= filter.MaxPrice);

            if (filter.IsAvailable)
                productList = productList.Where(x => x.AvailableAmount > 0);
            else
                productList = productList.Where(x => x.AvailableAmount == 0);

            productList = productList.Where(x => x.ProductTypeId == (int)filter.ProductType).ToList();
        }

        productDTOList = mapper.Map<List<ProductDTO>>(productList);

        return productDTOList;
    }
}
