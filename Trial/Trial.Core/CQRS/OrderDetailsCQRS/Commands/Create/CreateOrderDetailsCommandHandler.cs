using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.OrderDetails;


namespace Trial.Core.CQRS.OrderDetailsCQRS.Commands.Create;


public class CreateOrderDetailsCommandHandler : IRequestHandler<CreateOrderDetailsCommand, OrderDetailsDTO>
{
    private readonly IProductRepository productRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IOrderDetailsRepository orderDetailsRepository;
    private readonly IMapper mapper;

    public CreateOrderDetailsCommandHandler(IProductRepository productRepository,
        IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.orderRepository = orderRepository;
        this.orderDetailsRepository = orderDetailsRepository;
        this.mapper = mapper;
    }

    public async Task<OrderDetailsDTO> Handle(CreateOrderDetailsCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.CreateOrderDetailsDTO.ProductId, false);

        if (product == null)
            throw new NotFoundException(nameof(Product), request.CreateOrderDetailsDTO.ProductId);

        var order = await orderRepository.GetByIdAsync(request.CreateOrderDetailsDTO.OrderId, false);

        if (order == null)
            throw new NotFoundException(nameof(Order), request.CreateOrderDetailsDTO.OrderId);

        if (request.CreateOrderDetailsDTO.Amount > product.AvailableAmount)
            throw new ArgumentException("Available amount is less than requested.");

        var totalPrice = request.CreateOrderDetailsDTO.Amount * product.Price;
        var orderDetails = mapper.Map<OrderDetails>(request.CreateOrderDetailsDTO);
        orderDetails.TotalPrice = totalPrice;

        var position = (await orderDetailsRepository
            .GetAllAsync(x => x.OrderId == request.CreateOrderDetailsDTO.OrderId))
            .Count();
        orderDetails.Position = ++position;

        try
        {
            product.AvailableAmount -= orderDetails.Amount;
            productRepository.Update(product);
            await productRepository.SaveAsync();

            await orderDetailsRepository.AddAsync(orderDetails);
            await orderDetailsRepository.SaveAsync();
        }
        catch (Exception)
        { }

        var orderDetailsDTO = mapper.Map<OrderDetailsDTO>(orderDetails);

        return orderDetailsDTO;
    }
}
