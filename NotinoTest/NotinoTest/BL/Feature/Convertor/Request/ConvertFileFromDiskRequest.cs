using System.ComponentModel.DataAnnotations;
using NotinoTest.api.Convertor.Enums;
namespace NotinoTest.BL.Feature.Convertor.Request;

public class ConvertFileFromDiskRequest
{
    [Required]
    public string SourcePath { get; set; }
    [Required]
    public string TargerPath { get; set; }
    public DocumentTypeEnums ConvertTo { get; set; }
}