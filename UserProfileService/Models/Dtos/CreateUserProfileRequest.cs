﻿namespace UserProfileService.Models.Dtos;

public record CreateUserProfileRequest(
    string Name,
    DateOnly DateOfBirth,
    string Gender,
    string Role,
    string? UserId = null
);