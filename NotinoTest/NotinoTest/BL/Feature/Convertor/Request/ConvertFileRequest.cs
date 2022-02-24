using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.api.Convertor.Response;

public class ConvertFileRequest
{
   public DocumentTypeEnums ConvertTo { get; set; }
   public IFormFile Data { get; set; }
};