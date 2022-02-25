using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.api.Convertor.Services;

public interface IConvertorStrategy
{
    DocumentTypeEnums ProcessType { get; }
    string Serialize<T>(T serializeObject);
    T? Deserialize<T>(string value);
}