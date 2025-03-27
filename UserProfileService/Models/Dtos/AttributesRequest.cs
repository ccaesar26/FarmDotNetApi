namespace UserProfileService.Models.Dtos;

public record AttributesRequest(string UserProfileId, List<string> AttributeNames);