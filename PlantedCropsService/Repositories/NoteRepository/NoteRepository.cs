using PlantedCropsService.Data;
using PlantedCropsService.Models.Entities;
using PlantedCropsService.Repositories.GenericRepository;

namespace PlantedCropsService.Repositories.NoteRepository;

public class NoteRepository(CropsDbContext context) : GenericRepository<Note>(context), INoteRepository;