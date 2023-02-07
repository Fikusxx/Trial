using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.ProductCQRS.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private IProductTypeRepository productTypeRepository;
    private IProductRepository productRepository;
    private IMapper mapper;

    public UpdateProductCommandHandler(IProductTypeRepository productTypeRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        this.productTypeRepository = productTypeRepository;
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productType = await productTypeRepository.GetByIdAsync(request.UpdateProductDTO.ProductTypeId);

        if (productType == null)
            throw new NotFoundException(nameof(ProductType), request.UpdateProductDTO.ProductTypeId);

        var product = await productRepository.GetByIdAsync(request.UpdateProductDTO.Id, false);

        if (product == null)
            throw new NotFoundException(nameof(Product), request.UpdateProductDTO.Id);

        product = mapper.Map<Product>(request.UpdateProductDTO);
        productRepository.Update(product);
        await productRepository.SaveAsync();

        return Unit.Value;
    }
}
