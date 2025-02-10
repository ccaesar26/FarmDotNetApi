using FieldsService.Models.Entities;
using FieldsService.Repositories;
using NetTopologySuite.Geometries;

namespace FieldsService.Services;

public class FieldsService(IFieldsRepository fieldsRepository) : IFieldsService
{
    public async ValueTask<Field?> GetFieldByIdAsync(Guid id)
    {
        return await fieldsRepository.GetFieldByIdAsync(id);
    }

    public async Task<IEnumerable<Field?>> GetFieldsByFarmIdAsync(Guid farmId)
    {
        return await fieldsRepository.GetFieldsByFarmIdAsync(farmId);
    }

    public async ValueTask<Field> AddFieldAsync(Guid farmId, string fieldName, Polygon fieldBoundary)
    {
        if (await fieldsRepository.FieldExistsByNameAsync(farmId, fieldName))
        {
            throw new Exception("Field with this name already exists");
        }
        
        var field = new Field
        {
            FarmId = farmId,
            Name = fieldName,
            Boundary = fieldBoundary
        };
        
        return await fieldsRepository.AddFieldAsync(field);
    }

    public async Task UpdateFieldAsync(Guid id, Guid farmId, string fieldName, Polygon fieldBoundary)
    {
        var field = await fieldsRepository.GetFieldByIdAsync(id);
        if (field == null)
        {
            throw new Exception("Field not found");
        }
        
        field.FarmId = farmId;
        field.Name = fieldName;
        field.Boundary = fieldBoundary;
        
        await fieldsRepository.UpdateFieldAsync(field);
    }

    public async Task DeleteFieldAsync(Guid id)
    {
        await fieldsRepository.DeleteFieldAsync(id);
    }
    
    public async Task<bool> FieldExistsByNameAsync(Guid farmId, string name)
    {
        return await fieldsRepository.FieldExistsByNameAsync(farmId, name);
    }
}