using System.ComponentModel.DataAnnotations;
using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.api.Convertor.Handler;

public class ConvertFileFromDiskRequest
{
    [Required]
    public string SourcePath { get; set; }
    [Required]
    public string TargerPath { get; set; }
    public DocumentTypeEnums ConvertTo { get; set; }
}