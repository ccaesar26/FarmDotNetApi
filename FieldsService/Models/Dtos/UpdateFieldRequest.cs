using NetTopologySuite.Geometries;

namespace FieldsService.Models.Dtos;

public record UpdateFieldRequest(
    string FieldId,
    string FieldName,
    Polygon FieldBoundary
);