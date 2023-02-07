using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;


namespace Trial.Persistence.Repository;


public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
{
    public OrderDetailsRepository(ApplicationDbContext db) : base(db)
    { }

    public async Task<OrderDetails?> GetByIdAsync(int id, bool tracking = true)
    {
        if (tracking)
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<OrderDetails>> GetAllAsync(Expression<Func<OrderDetails, bool>> predicate = null)
    {
        if (predicate != null)
            return await dbSet.Where(predicate).ToListAsync();

        return dbSet.AsQueryable();
    }

    public async Task AddAsync(OrderDetails orderDetails)
    {
        await dbSet.AddAsync(orderDetails);
    }

    public void Update(OrderDetails orderDetails)
    {
        dbSet.Update(orderDetails);
    }

    public void Delete(OrderDetails orderDetails)
    {
        dbSet.Remove(orderDetails);
    }
}
