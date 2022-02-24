using System.Text;
using NotinoTest.api.Convertor.Enums;
using NotinoTest.api.Convertor.Models;
using NotinoTest.api.Convertor.Services;
using NotinoTest.Infrastructure.Error;
using OneOf;

namespace NotinoTest.api.Convertor;

public class ConvertService : IConvertorService
{
    private readonly ConvertResolver _resolver;
    private readonly ILogger<ConvertService> _logger;

    public ConvertService(ConvertResolver resolver, ILogger<ConvertService> logger)
    {
        _resolver = resolver;
        _logger = logger;
    }

    private (DocumentTypeEnums, bool) TryCheckFileType(string content)
    {
        if (content.IsJson())
            return (DocumentTypeEnums.Json, true);

        if (content.IsXml())
            return (DocumentTypeEnums.Xml, true);

        return (default, false);
    }
    
    public OneOf<string, ErrorType> Convert(string content, DocumentTypeEnums convertTo)
    {
        var (type, success) = TryCheckFileType(content);
        if (!success)
            return new ErrorType("Not supported type");

        try
        {
            var value = _resolver[type].Deserialize<Document>(content);
            return _resolver[convertTo].Serialize(value);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ErrorType("Deserialization Fail");
        }
    }
}