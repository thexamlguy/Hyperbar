using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hyperbar;

public class ConfigurationWriter<TConfiguration>(IConfigurationSource<TConfiguration> source,
    JsonSerializerOptions? serializerOptions = null) :
    IConfigurationWriter<TConfiguration>
    where TConfiguration :
    class, new()
{
    private static readonly Func<JsonSerializerOptions> defaultSerializerOptions = new(() =>
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new JsonStringEnumConverter() }
        };
    });

    public void Write(Action<TConfiguration> updateDelegate)
    {
        if ((TryGet(out TConfiguration? value) ? value : new TConfiguration()) is TConfiguration updatedValue)
        {
            updateDelegate?.Invoke(updatedValue);
            Write(updatedValue);
        }
    }

    public void Write(TConfiguration value)
    {
        if (!File.Exists(source.Path))
        {
            string? fileDirectoryPath = Path.GetDirectoryName(source.Path);
            if (!string.IsNullOrEmpty(fileDirectoryPath))
            {
                Directory.CreateDirectory(fileDirectoryPath);
            }

            File.WriteAllText(source.Path, "{}");
        }

        byte[] jsonContent = File.ReadAllBytes(source.Path);

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
        using FileStream stream = File.OpenWrite(source.Path);
        Utf8JsonWriter writer = new(stream, new JsonWriterOptions()
        {
            Indented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        writer.WriteStartObject();
        bool isWritten = false;
        JsonDocument optionsElement = JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(value, serializerOptions ?? defaultSerializerOptions()));

        foreach (JsonProperty element in jsonDocument.RootElement.EnumerateObject())
        {
            if (element.Name != source.Section)
            {
                element.WriteTo(writer);
                continue;
            }
            writer.WritePropertyName(element.Name);
            optionsElement.WriteTo(writer);
            isWritten = true;
        }

        if (!isWritten)
        {
            writer.WritePropertyName(source.Section);
            optionsElement.WriteTo(writer);
        }

        writer.WriteEndObject();
        writer.Flush();
        stream.SetLength(stream.Position);
    }

    private bool TryGet(out TConfiguration? value)
    {
        if (File.Exists(source.Path))
        {
            byte[] jsonContent = File.ReadAllBytes(source.Path);

            using JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
            if (jsonDocument.RootElement.TryGetProperty(source.Section, out JsonElement sectionValue))
            {
                value = JsonSerializer.Deserialize<TConfiguration>(sectionValue.ToString(), serializerOptions ?? defaultSerializerOptions());
                return true;
            }
        }

        value = default;
        return false;
    }
}