using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;
using Trial.Core.DTO.Customer;

namespace Trial.Core.CQRS.CustomerCQRS.Queries.GetCustomer;


public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDTO>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    public async Task<CustomerDTO> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.Id);

        var customerDTO = mapper.Map<CustomerDTO>(customer);

        return customerDTO;
    }
}
