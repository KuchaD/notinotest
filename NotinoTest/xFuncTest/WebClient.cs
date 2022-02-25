using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotinoTest.Infrastructure.Error;
using NotinoTest.Infrastructure.Serializer;
using JsonSerializer = NotinoTest.Infrastructure.Serializer.JsonSerializer;
using OneOf;

namespace FuncTest;

public class WebClient
{
    private static WebClient _instance;
    private IOptions<JsonSerializerOptions> _options;
    private ISerializer _serializer;
    private HttpClient _client;

    public IConfiguration Configuration { get; private set; }

    public static WebClient GetInstance()
    {
        return _instance ??= new WebClient();
    }

    public WebClient()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        JsonSerializerOptions options = new JsonSerializerOptions();
        JsonSerializer.SetupJsonOptions(options);
        _options = Options.Create(options);
        _serializer = new JsonSerializer(_options);

        _client = new HttpClient()
        {
            BaseAddress = new Uri(Configuration["Url"])
        };
    }

    public async Task<OneOf<T?, ErrorType>> CallAsync<T>(
        Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> func, CancellationToken cancellationToken)
    {
        var result = await func(_client, cancellationToken);
        var body = await result.Content.ReadAsStringAsync(cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            return _serializer.Deserialize<ErrorType>(body);
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            return default;
        }

        return _serializer.Deserialize<T>(body);
    }

    public async Task<OneOf<(Stream, string)?, ErrorType>> CallReturnFileAsync(
        Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> func, Stream stream,
        CancellationToken cancellationToken)
    {
        var result = await func(_client, cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            var body = await result.Content.ReadAsStringAsync(cancellationToken);
            return _serializer.Deserialize<ErrorType>(body);
        }

        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);

        await contentStream.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        return ( stream, result.Content.Headers.ContentDisposition!.FileName );
    }
}