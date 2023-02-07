using MediatR;
using Trial.Core.DTO.Product;

namespace Trial.Core.CQRS.ProductCQRS.Queries.GetProduct;

public class GetProductQuery : IRequest<ProductDTO>
{
    public int Id { get; set; }
}
