using FieldsService.Models.Entities;
using NetTopologySuite.Geometries;

namespace FieldsService.Services;

public interface IFieldsService
{
    public ValueTask<Field?> GetFieldByIdAsync(Guid id);
    
    public Task<IEnumerable<Field?>> GetFieldsByFarmIdAsync(Guid farmId);
    
    public ValueTask<Field> AddFieldAsync(Guid farmId, string fieldName, Polygon fieldBoundary);
    
    public Task UpdateFieldAsync(Guid id, Guid farmId, string fieldName, Polygon fieldBoundary);
    
    public Task DeleteFieldAsync(Guid id);
    
    public Task<bool> FieldExistsByNameAsync(Guid farmId, string name);
}