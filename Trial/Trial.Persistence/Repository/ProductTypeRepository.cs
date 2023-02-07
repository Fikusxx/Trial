using Microsoft.EntityFrameworkCore;
using Trial.Core.Contracts.Persistence;
using Trial.Core.Domain;

namespace Trial.Persistence.Repository;


public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
	public ProductTypeRepository(ApplicationDbContext db) : base(db)
	{ }

    public async Task<ProductType?> GetByIdAsync(int id)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ProductType>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }
}
