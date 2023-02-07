using AutoMapper;
using MediatR;
using Trial.Core.Common.Exceptions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Core.CQRS.CustomerCQRS.Commands.Delete;


public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.Id);

        customerRepository.Delete(customer);
        await customerRepository.SaveAsync();

        return Unit.Value;
    }
}
