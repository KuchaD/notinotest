using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using NotinoTest.api.Convertor.Enums;
using NotinoTest.api.Convertor.Models;
using NotinoTest.api.Convertor.Services;
using NotinoTest.Infrastructure.Serializer;
using Xunit;
using JsonSerializer = NotinoTest.Infrastructure.Serializer.JsonSerializer;

namespace UnitTest.BL.Feature.Convertor.Services;

public class JsonConvertorStrategyTest
{
    private IOptions<JsonSerializerOptions> _options;
    private readonly JsonConvertorStrategy _strategy;

    public JsonConvertorStrategyTest()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        JsonSerializer.SetupJsonOptions(options);
        _options = Options.Create(options);
        _strategy = new JsonConvertorStrategy(new JsonSerializer(_options));
    }

    [Fact]
    public void DeserializeDocument_successConvert()
    {
        var data = ConvertTestDataConst.Document1;
        var result = _strategy.Serialize(data);
        Assert.Equal("{\"title\":\"TestTitle\",\"text\":\"testText\"}", result);
    }
    
    [Fact]
    public void SerializeDocument_successConvert()
    {
        var data = "{\"title\":\"TestTitle\",\"text\":\"testText\"}";
        var result = _strategy.Deserialize<Document>(data);
        Assert.Equal(ConvertTestDataConst.Document1, result);
    }
}