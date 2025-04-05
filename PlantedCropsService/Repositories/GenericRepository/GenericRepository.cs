using Microsoft.EntityFrameworkCore;
using PlantedCropsService.Data;

namespace PlantedCropsService.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;

    protected GenericRepository(CropsDbContext context)
    {
        var context1 = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context1.Set<T>();
    }

    public async ValueTask<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async ValueTask<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async ValueTask AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async ValueTask<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
}