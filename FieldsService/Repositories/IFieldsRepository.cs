using FieldsService.Models.Entities;

namespace FieldsService.Repositories;

public interface IFieldsRepository
{
    ValueTask<Field?> GetFieldByIdAsync(Guid id);
    
    Task<List<Field?>> GetFieldsByFarmIdAsync(Guid farmId);
    
    ValueTask<Field> AddFieldAsync(Field field);
    
    ValueTask UpdateFieldAsync(Field field);
    
    ValueTask DeleteFieldAsync(Guid id);

    ValueTask<bool> FieldExistsByNameAsync(Guid farmId, string name);
}