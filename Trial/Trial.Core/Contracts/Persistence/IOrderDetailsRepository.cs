using System.Linq.Expressions;
using Trial.Core.Domain;


namespace Trial.Core.Contracts.Persistence;


public interface IOrderDetailsRepository : IGenericRepository<OrderDetails>
{
    public Task<OrderDetails?> GetByIdAsync(int id, bool tracking = true);
    public Task<IEnumerable<OrderDetails>> GetAllAsync(Expression<Func<OrderDetails, bool>> predicate = null);
    public Task AddAsync(OrderDetails orderDetails);
    public void Update(OrderDetails orderDetails);
    public void Delete(OrderDetails orderDetails);
}
