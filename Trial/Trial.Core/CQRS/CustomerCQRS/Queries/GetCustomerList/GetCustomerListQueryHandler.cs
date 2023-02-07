using AutoMapper;
using MediatR;
using Trial.Core.Contracts.Persistence;
using Trial.Core.DTO.Customer;


namespace Trial.Core.CQRS.CustomerCQRS.Queries.GetCustomerList;


public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, List<CustomerDTO>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public GetCustomerListQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    public async Task<List<CustomerDTO>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        var customerList = await customerRepository.GetAllAsync();
        var customerDTOList = mapper.Map<List<CustomerDTO>>(customerList);

        return customerDTOList;
    }
}
