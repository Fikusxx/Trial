using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.CustomerCQRS.Commands.Update;


public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.UpdateCustomerDTO.Id, false);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.UpdateCustomerDTO.Id);

        customer = mapper.Map<Customer>(request.UpdateCustomerDTO);

        customerRepository.Update(customer);
        await customerRepository.SaveAsync();

        return Unit.Value;
    }
}
