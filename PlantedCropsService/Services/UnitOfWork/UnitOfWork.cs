using PlantedCropsService.Data;

namespace PlantedCropsService.Services.UnitOfWork;

public class UnitOfWork(CropsDbContext context) : IUnitOfWork
{
    public async ValueTask<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}