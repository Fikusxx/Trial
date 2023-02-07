using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Product;

namespace Trial.Core.CQRS.ProductCQRS.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDTO>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product == null)
            throw new NotFoundException(nameof(Product), request.Id);

        var productDTO = mapper.Map<ProductDTO>(product);

        return productDTO;
    }
}
