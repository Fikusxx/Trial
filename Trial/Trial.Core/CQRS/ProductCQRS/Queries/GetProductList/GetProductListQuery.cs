using MediatR;
using Trial.Core.Common.Filtering;
using Trial.Core.DTO.Product;

namespace Trial.Core.CQRS.ProductCQRS.Queries.GetProductList;


public class GetProductListQuery : IRequest<List<ProductDTO>>
{
    public ProductFilter ProductFilter { get; set; }
}
