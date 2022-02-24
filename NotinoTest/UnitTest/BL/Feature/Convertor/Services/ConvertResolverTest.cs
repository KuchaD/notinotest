using System.Collections.Generic;
using Moq;
using NotinoTest.api.Convertor.Enums;
using NotinoTest.api.Convertor.Services;
using Xunit;

namespace UnitTest.BL.Feature.Convertor.Services;

public class ConvertResolverTest
{
    private ConvertResolver SetupResolver()
    {
        var json = new Mock<IConvertorStrategy>();
        json.SetupGet(x => x.ProcessType).Returns(DocumentTypeEnums.Json);
        var xml = new Mock<IConvertorStrategy>();
        xml.SetupGet(x => x.ProcessType).Returns(DocumentTypeEnums.Xml);

       return new ConvertResolver(new[]
        {
            json.Object,
            xml.Object
        });
    }
    
    [Theory]
    [InlineData(DocumentTypeEnums.Json)]
    [InlineData(DocumentTypeEnums.Xml)]
    public void ConvertResolver_returnCorrectStrategy(DocumentTypeEnums type)
    {
        var resolver = SetupResolver();
        Assert.Equal(type, resolver[type].ProcessType);
    }
    
    [Fact]
    public void ConvertResolver_NonExistStrategy_returnException()
    {
        var json = new Mock<IConvertorStrategy>();
        json.SetupGet(x => x.ProcessType).Returns(DocumentTypeEnums.Json);
        
        var resolver = new ConvertResolver(new[]
        {
            json.Object
        });

        Assert.Throws<KeyNotFoundException>(() => resolver[DocumentTypeEnums.Xml]);
    }
}