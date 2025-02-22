using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherService.Converters;

public class TimeOnlyConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var time = reader.GetString();
        return time != null ? TimeOnly.Parse(time) : default;
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}