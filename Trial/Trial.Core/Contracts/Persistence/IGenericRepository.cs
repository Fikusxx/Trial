
namespace Trial.Core.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    public Task SaveAsync();
}
