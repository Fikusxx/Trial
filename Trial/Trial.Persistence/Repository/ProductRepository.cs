using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Persistence.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext db) : base(db)
    { }

    public async Task<Product?> GetByIdAsync(int id, bool tracking = true)
    {
        if (tracking)
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate = null)
    {
        if (predicate != null)
            return await dbSet.Where(predicate).ToListAsync();

        return dbSet.AsQueryable();
    }

    public async Task AddAsync(Product product)
    {
        await dbSet.AddAsync(product);
    }

    public void Update(Product product)
    {
        dbSet.Update(product);
    }

    public void Delete(Product product)
    {
        dbSet.Remove(product);
    }
}
