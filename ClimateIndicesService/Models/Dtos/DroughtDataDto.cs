namespace ClimateIndicesService.Models.Dtos;

public record DroughtDataDto(
    DateTime Date,
    string RasterDataBase64
);