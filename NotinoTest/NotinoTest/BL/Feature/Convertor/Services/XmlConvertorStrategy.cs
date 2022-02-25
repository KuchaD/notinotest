using System.Xml;
using System.Xml.Serialization;
using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.api.Convertor.Services;

public class XmlConvertorStrategy : IConvertorStrategy
{
    public DocumentTypeEnums ProcessType => DocumentTypeEnums.Xml;

    public string Serialize<T>(T serializeObject)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var stringWriter = new StringWriter();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

        serializer.Serialize(xmlWriter, serializeObject);

        return stringWriter.ToString();
    }

    public T? Deserialize<T>(string value)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var stringReader = new StringReader(value);
        using var xmlReader = new XmlTextReader(stringReader);

        return (T)serializer.Deserialize(xmlReader);
    }
}