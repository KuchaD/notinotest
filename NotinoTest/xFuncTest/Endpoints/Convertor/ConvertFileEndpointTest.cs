using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NotinoTest.api.Convertor.Enums;
using NotinoTest.Infrastructure.Error;
using Xunit;

namespace FuncTest.Endpoints.Convertor;

public class ConvertFileEndpointTest : ConvertorBase
{
    private readonly string _endpoint = string.Format(route, "file");
    
    [Fact]
    public async Task CallEndpoint_FileDisk_SuccessConvertToJson()
    {
        var file = await File.ReadAllBytesAsync("./TestFiles/TestXml.xml");
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(DocumentTypeEnums.Json.ToString()), "ConvertTo");
        formData.Add(new ByteArrayContent(file), "Data", "\"test.xml\"");
        Stream resultStream = new MemoryStream();
        
        var response = await WebClient.GetInstance().CallReturnFileAsync(
            (client, ct) => client.PostAsync(_endpoint, formData, ct), resultStream,
            CancellationToken.None);
        
        Assert.IsNotType<ErrorType>(response.Value);
        var reader = new StreamReader(resultStream);
        var expectedResult = await reader.ReadToEndAsync();
        Assert.Equal(jsonData, expectedResult );
    }

    [Fact]
    public async Task CallEndpoint_FileDisk_SuccessConvertToXml()
    {
        var file = await File.ReadAllBytesAsync("./TestFiles/TestJson.json");
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(DocumentTypeEnums.Xml.ToString()), "ConvertTo");
        formData.Add(new ByteArrayContent(file), "Data", "\"test.json\"");
        Stream resultStream = new MemoryStream();
        
        var response = await WebClient.GetInstance().CallReturnFileAsync(
            (client, ct) => client.PostAsync(_endpoint, formData, ct), resultStream,
            CancellationToken.None);
        
        Assert.IsNotType<ErrorType>(response.Value);
        var reader = new StreamReader(resultStream);
        var expectedResult = await reader.ReadToEndAsync();
        Assert.Equal(xmlData, expectedResult );
    }
    
    [Fact]
    public async Task CallEndpoint_FileDisk_Failed()
    {
        var file = await File.ReadAllBytesAsync("./TestFiles/corruptedFile.json");
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(DocumentTypeEnums.Xml.ToString()), "ConvertTo");
        formData.Add(new ByteArrayContent(file), "Data", "\"test.json\"");
        Stream resultStream = new MemoryStream();
        
        var response = await WebClient.GetInstance().CallReturnFileAsync(
            (client, ct) => client.PostAsync(_endpoint, formData, ct), resultStream,
            CancellationToken.None);
        
        Assert.IsType<ErrorType>(response.Value);
    }
}