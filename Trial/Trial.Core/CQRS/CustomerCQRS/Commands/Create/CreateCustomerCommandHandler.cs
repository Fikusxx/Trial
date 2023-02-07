using AutoMapper;
using MediatR;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Customer;

namespace Trial.Core.CQRS.CustomerCQRS.Commands.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDTO>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    public async Task<CustomerDTO> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = mapper.Map<Customer>(request.CreateCustomerDTO);
        await customerRepository.AddAsync(customer);
        await customerRepository.SaveAsync();

        var customerDTO = mapper.Map<CustomerDTO>(customer);

        return customerDTO;
    }
}
