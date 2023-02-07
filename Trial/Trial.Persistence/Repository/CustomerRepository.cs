using Microsoft.EntityFrameworkCore;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Persistence.Repository;


public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext db) : base(db)
    { }

    public async Task<Customer?> GetByIdAsync(int id, bool tracking)
    {
        if (tracking)
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await dbSet.AddAsync(customer);
    }

    public void Update(Customer customer)
    {
        dbSet.Update(customer);
    }

    public void Delete(Customer customer)
    {
        dbSet.Remove(customer);
    }
}
