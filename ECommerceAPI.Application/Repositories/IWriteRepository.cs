using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T model);
    Task<bool> AddRangeAsync(List<T> data);
    bool Remove(T model);
    Task<bool> RemoveAsync(string id);
    bool RemoveRange(List<T> data);
    bool Update(T model);
    Task<int> SaveChangesAsync();
}