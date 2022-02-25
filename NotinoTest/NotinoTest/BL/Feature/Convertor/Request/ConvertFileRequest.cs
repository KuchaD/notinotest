using NotinoTest.api.Convertor.Enums;
namespace NotinoTest.BL.Feature.Convertor.Request;

public class ConvertFileRequest
{
   public DocumentTypeEnums ConvertTo { get; set; }
   public IFormFile Data { get; set; }
};