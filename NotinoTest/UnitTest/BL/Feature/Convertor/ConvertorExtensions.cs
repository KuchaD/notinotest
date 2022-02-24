using NotinoTest.api.Convertor;
using Xunit;

namespace UnitTest.BL.Feature.Convertor;

public class ConvertorExtensionsTest
{
    [Theory]
    [InlineData("{ \"Test\": \"test\" }")]
    public void isJson_Success(string data)
    {
        Assert.True(data.IsJson());
    }

    [Theory]
    [InlineData("<Text>Text<Text>")]
    public void isXml_Success(string data)
    {
        Assert.True(data.IsXml());
    }
}