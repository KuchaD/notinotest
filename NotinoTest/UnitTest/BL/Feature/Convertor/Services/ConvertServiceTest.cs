using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using NotinoTest.api.Convertor;
using NotinoTest.api.Convertor.Enums;
using NotinoTest.api.Convertor.Services;
using NotinoTest.Infrastructure.Error;
using Xunit;

namespace UnitTest.BL.Feature.Convertor.Services;

public class ConvertServiceTest
{
    private const string DocumentXml =
        "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Title>TestTitle</Title><Text>testText</Text></Document>";

    private const string DocumentJson = "{\"title\":\"TestTitle\",\"text\":\"testText\"}";

    private ConvertService SetupService()
    {
        var json = new Mock<IConvertorStrategy>();
        json.SetupGet(x => x.ProcessType).Returns(DocumentTypeEnums.Json);
        json.Setup(x => x.Serialize(It.IsAny<object>())).Returns(DocumentJson);
        
        var xml = new Mock<IConvertorStrategy>();
        xml.SetupGet(x => x.ProcessType).Returns(DocumentTypeEnums.Xml);
        xml.Setup(x => x.Serialize(It.IsAny<object>())).Returns(DocumentXml);

        var resolver = new ConvertResolver(new[]
        {
            json.Object,
            xml.Object
        });
        
       return new ConvertService(resolver, new  Mock<ILogger<ConvertService>>().Object );
    }

    [Fact]
    public void ConvertService_NotSupportedTypeError()
    {
        ConvertService convertService = SetupService();
        var result = convertService.Convert("Test", DocumentTypeEnums.Json);
        
        Assert.IsType<ErrorType>(result.Match(response => response,e => e));
        Assert.Equal("Not supported type", result.AsT1.ErrorMessage);
    }
    
    [Fact]
    public void ConvertService_SuccessfulConvert()
    {
        ConvertService convertService = SetupService();
        var result = convertService.Convert(DocumentJson, DocumentTypeEnums.Xml);
        var resulMatch = result.Match(r => r, e => e.ErrorMessage);
        Assert.Equal(DocumentXml, resulMatch);
    }
}