using MediatR;
using Trial.Core.DTO.Product;


namespace Trial.Core.CQRS.ProductCQRS.Commands.Update;


public class UpdateProductCommand : IRequest
{
    public UpdateProductDTO UpdateProductDTO { get; set; }
}
