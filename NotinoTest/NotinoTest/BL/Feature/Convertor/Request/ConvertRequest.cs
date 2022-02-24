using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.api.Convertor.Response;

public class ConvertRequest
{
    public string Content { get; set; }
    public DocumentTypeEnums To { get; set; }
}