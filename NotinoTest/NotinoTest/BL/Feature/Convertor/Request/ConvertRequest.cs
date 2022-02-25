using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.BL.Feature.Convertor.Request;

public class ConvertRequest
{
    public string Content { get; set; }
    public DocumentTypeEnums To { get; set; }
}