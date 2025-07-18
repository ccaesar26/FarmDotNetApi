﻿using FarmProfileService.Models.Entities;

namespace FarmProfileService.Repositories;

public interface IFarmProfileRepository
{
    ValueTask<FarmProfile?> GetByIdAsync(Guid id);
    
    ValueTask<FarmProfile> AddAsync(FarmProfile farmProfile);
    
    ValueTask UpdateAsync(FarmProfile farmProfile);
    
    ValueTask DeleteAsync(Guid id);
}