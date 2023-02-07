using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.ProductCQRS.Commands.Delete;


public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>    
{
    private IProductRepository productRepository;

	public DeleteProductCommandHandler(IProductRepository productRepository)
	{
		this.productRepository = productRepository;
	}

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product == null)
            throw new NotFoundException(nameof(Product), request.Id);

        productRepository.Delete(product);
        await productRepository.SaveAsync();

        return Unit.Value;
    }
}
