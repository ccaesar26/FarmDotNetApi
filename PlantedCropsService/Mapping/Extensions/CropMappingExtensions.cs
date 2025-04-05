using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using PlantedCropsService.Models.Dtos;
using PlantedCropsService.Models.Entities;

namespace PlantedCropsService.Mapping.Extensions;

public static class CropMappingExtensions
{
    public static CropDto ToDto(this Crop crop) => new CropDto
    (
        crop.Id,
        crop.Name,
        crop.BinomialName,
        crop.CultivatedVariety,
        crop.ImageLink,
        crop.Perrenial,
        crop.ExpectedFirstHarvestDate,
        crop.ExpectedLastHarvestDate,
        crop.FieldId,
        crop.Surface?.AsText(),
        crop.Area,
        crop.CropCatalogId
    );

    public static Crop ToEntity(this CropCreateDto cropCreateDto) => new Crop
    {
        Name = cropCreateDto.Name,
        BinomialName = cropCreateDto.BinomialName,
        CultivatedVariety = cropCreateDto.CultivatedVariety,
        ImageLink = cropCreateDto.ImageLink,
        Perrenial = cropCreateDto.Perrenial,
        ExpectedFirstHarvestDate = cropCreateDto.ExpectedFirstHarvestDate,
        ExpectedLastHarvestDate = cropCreateDto.ExpectedLastHarvestDate,
        FieldId = cropCreateDto.FieldId,
        Surface = cropCreateDto.Surface != null ? ConvertGeoJsonToPolygon(cropCreateDto.Surface) : null,
        Area = cropCreateDto.Area,
        CropCatalogId = cropCreateDto.CropCatalogId
    };
    
    public static Crop ToEntity(this CropUpdateDto cropUpdateDto) => new Crop
    {
        Id = cropUpdateDto.Id,
        Name = cropUpdateDto.Name,
        BinomialName = cropUpdateDto.BinomialName,
        CultivatedVariety = cropUpdateDto.CultivatedVariety,
        ImageLink = cropUpdateDto.ImageLink,
        Perrenial = cropUpdateDto.Perrenial,
        ExpectedFirstHarvestDate = cropUpdateDto.ExpectedFirstHarvestDate,
        ExpectedLastHarvestDate = cropUpdateDto.ExpectedLastHarvestDate,
        FieldId = cropUpdateDto.FieldId,
        Surface = cropUpdateDto.Surface != null ? ConvertGeoJsonToPolygon(cropUpdateDto.Surface) : null,
        Area = cropUpdateDto.Area,
        CropCatalogId = cropUpdateDto.CropCatalogId
    };

    public static Polygon ConvertGeoJsonToPolygon(string geoJson)
    {
        if (string.IsNullOrWhiteSpace(geoJson))
            throw new ArgumentException("GeoJSON cannot be null or empty", nameof(geoJson));

        var reader = new GeoJsonReader();
        var geometry = reader.Read<Geometry>(geoJson);

        if (geometry is Polygon polygon)
        {
            return polygon;
        }

        throw new InvalidOperationException("Provided GeoJSON does not represent a valid Polygon.");
    }
}