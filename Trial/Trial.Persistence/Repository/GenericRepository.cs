using Microsoft.EntityFrameworkCore;
using Trial.Core.Contracts.Persistence;

namespace Trial.Persistence.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext db;
    protected DbSet<T> dbSet;

    public GenericRepository(ApplicationDbContext db)
    {
        this.db = db;
        this.dbSet = db.Set<T>();
    }

    public async Task SaveAsync()
    {
        await db.SaveChangesAsync();
    }
}
