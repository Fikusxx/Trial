using System.Linq.Expressions;
using Trial.Core.Domain;


namespace Trial.Core.Contracts.Persistence;


public interface IOrderRepository : IGenericRepository<Order>
{
    public Task<Order?> GetByIdAsync(int id, bool tracking = true);
    public Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null);
    public Task AddAsync(Order order);
    public void Update(Order order);
    public void Delete(Order order);
}
