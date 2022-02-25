using System.IO;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor.Enums;
using NotinoTest.BL.Feature.Convertor.Request;
using NotinoTest.Infrastructure.Error;
using Xunit;

namespace FuncTest.Endpoints.Convertor;

public class ConvertFileFromDiskEndpointTest : ConvertorBase
{
    private readonly string _endpoint = string.Format(_route, "file-disk");
    private string _path = Path.GetTempPath();

    [Fact]
    public async Task CallEndpoint_DiskFile_SuccessConvertToJson()
    {
        var targetPath = Path.Join(_path, "targetFile.xml");
        var response = await WebClient.GetInstance().CallAsync<IActionResult>(
            (client, ct) => client.PostAsJsonAsync(_endpoint,
                new ConvertFileFromDiskRequest()
                {
                    ConvertTo = DocumentTypeEnums.Json,
                    SourcePath = Path.GetFullPath("./TestFiles/TestXml.xml"),
                    TargerPath = targetPath

                }, ct),
            CancellationToken.None);

        Assert.IsNotType<ErrorType>(response.Value);
        await using var fileStream = File.OpenRead(targetPath);
        using var sr = new StreamReader(fileStream);
        var expectedResult = await sr.ReadToEndAsync();
        Assert.Equal(_jsonData, expectedResult);
    }

    [Fact]
    public async Task CallEndpoint_File_SuccessConvertToXml()
    {
        var targetPath = Path.Join(_path, "targetFile.json");
        var response = await WebClient.GetInstance().CallAsync<IActionResult>(
            (client, ct) => client.PostAsJsonAsync(_endpoint,
                new ConvertFileFromDiskRequest()
                {
                    ConvertTo = DocumentTypeEnums.Xml,
                    SourcePath = Path.GetFullPath("./TestFiles/TestJson.json"),
                    TargerPath = targetPath

                }, ct),
            CancellationToken.None);

        Assert.IsNotType<ErrorType>(response.Value);
        await using var fileStream = File.OpenRead(targetPath);
        using var sr = new StreamReader(fileStream);
        var expectedResult = await sr.ReadToEndAsync();
        Assert.Equal(_xmlData, expectedResult);
    }

    [Fact]
    public async Task CallEndpoint_File_Failed()
    {
        var targetPath = Path.Join(_path, "targetFile.xml");
        var response = await WebClient.GetInstance().CallAsync<IActionResult>(
            (client, ct) => client.PostAsJsonAsync(_endpoint,
                new ConvertFileFromDiskRequest()
                {
                    ConvertTo = DocumentTypeEnums.Xml,
                    SourcePath = Path.GetFullPath("./TestFiles/corruptedFile.json"),
                    TargerPath = targetPath

                }, ct),
            CancellationToken.None);

        Assert.IsType<ErrorType>(response.Value);
    }
}