using System.Linq.Expressions;
using Trial.Core.Domain;

namespace Trial.Core.Contracts.Persistence;

public interface IProductRepository : IGenericRepository<Product>
{
    public Task<Product?> GetByIdAsync(int id, bool tracking = true);
    public Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate = null);
    public Task AddAsync (Product product);
    public void Update (Product product);
    public void Delete (Product product);
}
