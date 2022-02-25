using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using NotinoTest.api.Convertor.Enums;
using NotinoTest.api.Convertor.Response;
using NotinoTest.BL.Feature.Convertor.Request;
using NotinoTest.Infrastructure.Error;
using Xunit;

namespace FuncTest.Endpoints.Convertor;

public class ConvertTextEndpointTest : ConvertorBase
{
    private readonly string _endpoint = string.Format(_route, "text");

    [Fact]
    public async Task CallEndpoint_Text_SuccessConvertToJson()
    {
        var result = await WebClient.GetInstance().CallAsync<ResponseContent>(
            (client, ct) => client.PostAsJsonAsync(_endpoint,
                new ConvertRequest() { Content = _jsonData, To = DocumentTypeEnums.Xml }, ct),
            CancellationToken.None);

        Assert.IsNotType<ErrorType>(result.Value);
        Assert.Equal(_xmlData, result.AsT0.ConvertString);
    }

    [Fact]
    public async Task CallEndpoint_Text_SuccessConvertToXml()
    {
        var result = await WebClient.GetInstance().CallAsync<ResponseContent>(
            (client, ct) => client.PostAsJsonAsync(_endpoint,
                new ConvertRequest() { Content = _xmlData, To = DocumentTypeEnums.Json }, ct),
            CancellationToken.None);

        Assert.IsNotType<ErrorType>(result.Value);
    }

    [Fact]
    public async Task CallEndpoint_Text_FailedConvert()
    {
        var result = await WebClient.GetInstance().CallAsync<ResponseContent>(
            (client, ct) => client.PostAsJsonAsync(_endpoint,
                new ConvertRequest() { Content = "Bad data", To = DocumentTypeEnums.Json }, ct),
            CancellationToken.None);

        Assert.IsType<ErrorType>(result.Value);
        Assert.True(result.AsT1.ErrorMessage.Contains("Not supported type"));
    }

}