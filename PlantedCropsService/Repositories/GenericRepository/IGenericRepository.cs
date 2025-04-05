namespace PlantedCropsService.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    ValueTask<T?> GetByIdAsync(Guid id);
    ValueTask<IEnumerable<T>> GetAllAsync();
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    ValueTask<bool> ExistsAsync(Guid id);
}