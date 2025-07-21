using Newtonsoft.Json;

namespace LexHarvester.Helper.Utils.JsonUtils;

public class SafeDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            var str = reader.Value as string;
            if (!string.IsNullOrEmpty(str) && DateTime.TryParse(str, out var date))
            {
                return date;
            }
        }
        else if (reader.TokenType == JsonToken.Date)
        {
            return Convert.ToDateTime(reader.Value);
        }

        return null; // Geçersizse null ata
    }

    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        if (value.HasValue)
            writer.WriteValue(value.Value.ToString("o"));
        else
            writer.WriteNull();
    }
}