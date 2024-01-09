﻿using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hyperbar;

public interface IConfigurationReader<TConfiguration>
    where TConfiguration :
    class, new()
{
    TConfiguration Read();
}

public class ConfigurationReader<TConfiguration>(string path,
    string section,
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
        if (File.Exists(path))
        {
            byte[] jsonContent = File.ReadAllBytes(path);

            using JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
            if (jsonDocument.RootElement.TryGetProperty(section, out JsonElement sectionValue))
            {
                value = JsonSerializer.Deserialize<TConfiguration>(sectionValue.ToString(), serializerOptions ?? defaultSerializerOptions());
                return true;
            }
        }

        value = default;
        return false;
    }
}

 public class ConfigurationWriter<TConfiguration>(string path,
    string section,
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
        if (!File.Exists(path))
        {
            string? fileDirectoryPath = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(fileDirectoryPath))
            {
                Directory.CreateDirectory(fileDirectoryPath);
            }

            File.WriteAllText(path, "{}");
        }

        byte[] jsonContent = File.ReadAllBytes(path);

        using JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
        using FileStream stream = File.OpenWrite(path);
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
            if (element.Name != section)
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
            writer.WritePropertyName(section);
            optionsElement.WriteTo(writer);
        }

        writer.WriteEndObject();
        writer.Flush();
        stream.SetLength(stream.Position);
    }

    private bool TryGet(out TConfiguration? value)
    {
        if (File.Exists(path))
        {
            byte[] jsonContent = File.ReadAllBytes(path);

            using JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
            if (jsonDocument.RootElement.TryGetProperty(section, out JsonElement sectionValue))
            {
                value = JsonSerializer.Deserialize<TConfiguration>(sectionValue.ToString(), serializerOptions ?? defaultSerializerOptions());
                return true;
            }
        }

        value = default;
        return false;
    }
}