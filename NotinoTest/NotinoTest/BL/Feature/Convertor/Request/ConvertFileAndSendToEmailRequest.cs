using System.ComponentModel.DataAnnotations;
using NotinoTest.api.Convertor.Enums;
namespace NotinoTest.BL.Feature.Convertor.Request;

public class ConvertFileAndSendToEmailRequest
{
    public DocumentTypeEnums ConvertTo { get; set; }
    public IFormFile File { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
}