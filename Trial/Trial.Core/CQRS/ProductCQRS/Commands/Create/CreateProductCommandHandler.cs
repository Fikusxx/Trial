using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Product;


namespace Trial.Core.CQRS.ProductCQRS.Commands.Create;


public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
{
    private IProductTypeRepository productTypeRepository;
    private IProductRepository productRepository;
    private IMapper mapper;

    public CreateProductCommandHandler(IProductTypeRepository productTypeRepository, 
        IProductRepository productRepository, IMapper mapper)
    {
        this.productTypeRepository = productTypeRepository;
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productType = await productTypeRepository.GetByIdAsync(request.CreateProductDTO.ProductTypeId);

        if (productType == null)
            throw new NotFoundException(nameof(ProductType), request.CreateProductDTO.ProductTypeId);

        var product = mapper.Map<Product>(request.CreateProductDTO);
        await productRepository.AddAsync(product);
        await productRepository.SaveAsync();
        var productDTO = mapper.Map<ProductDTO>(product);

        return productDTO;
    }
}
