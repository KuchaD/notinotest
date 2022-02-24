using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace NotinoTest.Infrastructure.Serializer;

public class JsonSerializer : ISerializer
{
    public static void SetupJsonOptions(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.AllowTrailingCommas = true;
        options.IgnoreNullValues = true;
        options.PropertyNameCaseInsensitive = true;
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        options.IncludeFields = true;

        options.Converters.Add(new JsonStringEnumConverter());
    }

    private readonly JsonSerializerOptions options;

    public JsonSerializer(IOptions<JsonSerializerOptions> jsonOptionsSnapshot)
    {
        options = jsonOptionsSnapshot.Value;
    }

    public object? Deserialize(string message, Type messageType)
    {
        return System.Text.Json.JsonSerializer.Deserialize(message, messageType, options);
    }

    public T? Deserialize<T>(string message)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(message, options);
    }

    public string Serialize(object? data)
    {
        return System.Text.Json.JsonSerializer.Serialize(data, options);
    }
}   