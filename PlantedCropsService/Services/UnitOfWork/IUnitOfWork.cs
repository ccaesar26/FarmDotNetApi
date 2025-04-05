namespace PlantedCropsService.Services.UnitOfWork;

public interface IUnitOfWork
{
    ValueTask<int> SaveChangesAsync();
}