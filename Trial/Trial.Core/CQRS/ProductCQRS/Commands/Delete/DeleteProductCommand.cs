using MediatR;


namespace Trial.Core.CQRS.ProductCQRS.Commands.Delete;


public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }
}
