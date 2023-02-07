using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;


namespace Trial.Persistence.Repository;


public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext db) : base(db)
    { }

    public async Task<Order?> GetByIdAsync(int id, bool tracking = true)
    {
        if (tracking)
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null)
    {
        if (predicate != null)
            return await dbSet.Where(predicate).ToListAsync();

        return dbSet.AsQueryable();
    }

    public async Task AddAsync(Order order)
    {
        await dbSet.AddAsync(order);
    }

    public void Update(Order order)
    {
        dbSet.Update(order);
    }

    public void Delete(Order order)
    {
        dbSet.Remove(order);
    }
}
