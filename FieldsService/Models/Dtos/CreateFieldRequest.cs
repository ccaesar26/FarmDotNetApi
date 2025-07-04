﻿using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace FieldsService.Models.Dtos;

public class CreateFieldRequest {
    public required string FieldName { get; init; }
    
    [JsonConverter(typeof(NetTopologySuite.IO.Converters.GeometryConverter))]
    public required Polygon FieldBoundary { get; init; }
}