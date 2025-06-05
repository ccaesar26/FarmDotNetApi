using CropIdService.Models.Entities;

namespace CropIdService.Repositories.IdEntryRepository;

public interface IIdEntryRepository
{
    ValueTask<IdEntry> AddIdEntryAsync(IdEntry idEntry);
    ValueTask<IdEntry?> GetIdEntryByIdAsync(Guid id);
    ValueTask<IEnumerable<IdEntry>> GetAllIdEntriesByFarmIdAsync(Guid farmId);
    ValueTask<IdEntry?> UpdateIdEntryAsync(IdEntry idEntry);
    ValueTask<bool> DeleteIdEntryAsync(Guid id);
}