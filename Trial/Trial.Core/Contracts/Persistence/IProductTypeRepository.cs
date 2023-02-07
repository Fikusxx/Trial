using Trial.Core.Domain;

namespace Trial.Core.Contracts.Persistence;

public interface IProductTypeRepository
{
    public Task<ProductType?> GetByIdAsync(int id);
    public Task<List<ProductType>> GetAllAsync();
}
