using Markel.Application.Entities;
using Markel.Infrastructure.Data;

namespace Markel.Infrastructure.Repositories;

public abstract class GenericRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected GenericRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }

    public virtual void Add(T entity)
    {
        DbContext.Set<T>().Add(entity);
    }

    public virtual void Remove(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }
    
    public virtual void Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
    }
}