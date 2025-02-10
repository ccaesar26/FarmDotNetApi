using FieldsService.Data;
using FieldsService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldsService.Repositories;

public class FieldsRepository(FieldsDbContext context) : IFieldsRepository
{
    public async ValueTask<Field?> GetFieldByIdAsync(Guid id)
    {
        return await context.Fields.FindAsync(id);
    }

    public async Task<List<Field?>> GetFieldsByFarmIdAsync(Guid farmId)
    {
        return await context.Fields
            .Where(f => f != null && f.FarmId == farmId)
            .ToListAsync();
    }

    public async ValueTask<Field> AddFieldAsync(Field field)
    {
        await context.Fields.AddAsync(field);
        await context.SaveChangesAsync();
        return field;
    }

    public async ValueTask UpdateFieldAsync(Field field)
    {
        context.Fields.Update(field);
        await context.SaveChangesAsync();
    }

    public async ValueTask DeleteFieldAsync(Guid id)
    {
        var field = await context.Fields.FindAsync(id);
        if (field != null)
        {
            context.Fields.Remove(field);
            await context.SaveChangesAsync();
        }
    }

    public async ValueTask<bool> FieldExistsByNameAsync(Guid farmId, string name)
    {
        return await context.Fields
            .AnyAsync(f => f != null && f.FarmId == farmId && f.Name == name);
    }
}