using NotinoTest.api.Convertor.Enums;
using NotinoTest.api.Convertor.Services;
using NotinoTest.Infrastructure.Serializer;

namespace NotinoTest.BL.Feature.Convertor.Services;

public class JsonConvertorStrategy : IConvertorStrategy
{
    private readonly ISerializer _serializer;
    public DocumentTypeEnums ProcessType => DocumentTypeEnums.Json;

    public JsonConvertorStrategy(ISerializer serializer)
    {
        _serializer = serializer;
    }

    public string Serialize<T>(T serializeObject)
    {
        return _serializer.Serialize(serializeObject);
    }

    public T? Deserialize<T>(string value)
    {
        return _serializer.Deserialize<T>(value);
    }
}