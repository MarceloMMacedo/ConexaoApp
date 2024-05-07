using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConexaoApp.FirmRegistry.Models.Converters;


public class EnumDescriptionConverter<TEnum> : JsonConverter<TEnum>
{
    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && typeof(TEnum).IsEnum)
        {
            string enumDescription = reader.GetString();
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                string description = GetEnumDescription(value);
                if (string.Equals(enumDescription, description, StringComparison.OrdinalIgnoreCase))
                {
                    return value;
                }
            }
        }
        return default;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        string description = GetEnumDescription(value);
        writer.WriteStringValue(description);
    }

    private string? GetEnumDescription(TEnum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        if (attributes != null && attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        return value.ToString();
    }
}

