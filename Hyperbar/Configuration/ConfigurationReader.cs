using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hyperbar;

public class ConfigurationReader<TConfiguration>(IConfigurationSource<TConfiguration> source,
    JsonSerializerOptions? serializerOptions = null) :
    IConfigurationReader<TConfiguration>
    where TConfiguration :
    class, new()
{
    private static readonly Func<JsonSerializerOptions> defaultSerializerOptions = new(() =>
    {
        return new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = 
            { 
                new JsonStringEnumConverter() 
            }
        };
    });

    public TConfiguration Read()
    {
        if ((TryGet(out TConfiguration? value) ? value : new TConfiguration()) is TConfiguration configuration)
        {
            return configuration;
        }

        return new TConfiguration();
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
