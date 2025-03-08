namespace UserProfileService.Models.Dtos;

public record AssignAttributesRequest(string UserProfileId, List<string> AttributeNames);