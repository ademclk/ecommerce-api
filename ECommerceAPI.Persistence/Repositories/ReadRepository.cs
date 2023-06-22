using System.Linq.Expressions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ECommerceApiDbContext _context;

    public ReadRepository(ECommerceApiDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool isTracking = true)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();
        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool isTracking = true)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();
        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool isTracking = true)
    {
        var query = Table.AsQueryable();
        if(!isTracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    public async Task<T> GetByIdAsync(string id, bool isTracking = true)
    {
        var query = Table.AsQueryable();
        if(!isTracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }
}