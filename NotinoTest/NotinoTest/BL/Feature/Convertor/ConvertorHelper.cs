using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.api.Convertor;

public static class ConvertorHelper
{
    public static Dictionary<DocumentTypeEnums, Func<string, string>> FileNameStrategy = new()
    {
        { DocumentTypeEnums.Json, name => $"{name}.json" },
        { DocumentTypeEnums.Xml, name => $"{name}.xml" }
    };
}