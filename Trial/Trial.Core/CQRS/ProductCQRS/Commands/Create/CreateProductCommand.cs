using MediatR;
using Trial.Core.DTO.Product;


namespace Trial.Core.CQRS.ProductCQRS.Commands.Create;


public class CreateProductCommand : IRequest<ProductDTO>
{
    public CreateProductDTO CreateProductDTO { get; set; }
}
