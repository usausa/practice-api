namespace Example.Web.Infrastructure.Json;

using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString()!;
        try
        {
            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }
        catch (Exception e)
        {
            throw new JsonException($"Invalid date format. value=[{value}]", e);
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture));
    }
}
