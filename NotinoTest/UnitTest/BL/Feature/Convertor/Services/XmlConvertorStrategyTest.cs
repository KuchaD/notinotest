using System.Text.Json;
using Microsoft.Extensions.Options;
using NotinoTest.api.Convertor.Models;
using NotinoTest.api.Convertor.Services;
using Xunit;
using JsonSerializer = NotinoTest.Infrastructure.Serializer.JsonSerializer;

namespace UnitTest.BL.Feature.Convertor.Services;

public class XmlConvertorStrategyTest
{
    private readonly XmlConvertorStrategy _strategy;
    private const string DocumentXml =
        "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Title>TestTitle</Title><Text>testText</Text></Document>";
    
    public XmlConvertorStrategyTest()
    {
        _strategy = new XmlConvertorStrategy();
    }

    [Fact]
    public void DeserializeDocument_successConvert()
    {
        var data = ConvertTestDataConst.Document1;
        var result = _strategy.Serialize(data);
        Assert.Equal(DocumentXml, result);
    }
    
    [Fact]
    public void SerializeDocument_successConvert()
    {
        var data = DocumentXml;
        var result = _strategy.Deserialize<Document>(data);
        Assert.Equal(ConvertTestDataConst.Document1, result);
    }
}