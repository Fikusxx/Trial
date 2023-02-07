using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.OrderDetailsCQRS.Commands.Update;


public class UpdateOrderDetailsCommandHandler : IRequestHandler<UpdateOrderDetailsCommand>
{
    private readonly IProductRepository productRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IOrderDetailsRepository orderDetailsRepository;
    private readonly IMapper mapper;

    public UpdateOrderDetailsCommandHandler(IProductRepository productRepository,
        IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.orderRepository = orderRepository;
        this.orderDetailsRepository = orderDetailsRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateOrderDetailsCommand request, CancellationToken cancellationToken)
    {
        var orderDetails = await orderDetailsRepository.GetByIdAsync(request.UpdateOrderDetailsDTO.Id, false);

        if (orderDetails == null)
            throw new NotFoundException(nameof(OrderDetails), request.UpdateOrderDetailsDTO.Id);

        var product = await productRepository.GetByIdAsync(request.UpdateOrderDetailsDTO.ProductId, false);

        if (product == null)
            throw new NotFoundException(nameof(Product), request.UpdateOrderDetailsDTO.ProductId);

        if (product.Id != request.UpdateOrderDetailsDTO.ProductId)
            throw new ArgumentException("Cant change product.");

        var order = await orderRepository.GetByIdAsync(request.UpdateOrderDetailsDTO.OrderId, false);

        if (order == null)
            throw new NotFoundException(nameof(Order), request.UpdateOrderDetailsDTO.OrderId);

        var redefinedAmount = product.AvailableAmount + orderDetails.Amount;

        if (request.UpdateOrderDetailsDTO.Amount > redefinedAmount)
            throw new ArgumentException("Available amount is less than requested.");

        try
        {
            // Define product amount difference and update the product accordingly
            int amountDifference = orderDetails.Amount - request.UpdateOrderDetailsDTO.Amount;

            if (amountDifference > 0) // if we want less
                product.AvailableAmount += amountDifference;
            else if (amountDifference < 0) // if we want more
                product.AvailableAmount -= Math.Abs(amountDifference);

            productRepository.Update(product);
            await productRepository.SaveAsync();

            // Define new total price and position
            var totalPrice = request.UpdateOrderDetailsDTO.Amount * product.Price;
            var position = orderDetails.Position;
            orderDetails = mapper.Map<OrderDetails>(request.UpdateOrderDetailsDTO);
            orderDetails.TotalPrice = totalPrice;
            orderDetails.Position = position;

            orderDetailsRepository.Update(orderDetails);
            await orderDetailsRepository.SaveAsync();
        }
        catch (Exception)
        { }

        return Unit.Value;
    }
}
