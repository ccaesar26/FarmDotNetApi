using NetTopologySuite.Geometries;

namespace FieldsService.Models.Dtos;

public record UpdateFieldRequest(Guid FarmId, string FieldName, Polygon FieldBoundary);