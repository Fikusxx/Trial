using Trial.Core.Domain;


namespace Trial.Core.Contracts.Persistence;


public interface ICustomerRepository : IGenericRepository<Customer>
{
    public Task<Customer?> GetByIdAsync(int id, bool tracking = true);
    public Task<IEnumerable<Customer>> GetAllAsync();
    public Task AddAsync(Customer customer);    
    public void Update(Customer customer);
    public void Delete(Customer customer);
}
