using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor;
using NotinoTest.api.Convertor.Response;
using NotinoTest.Infrastructure;
using NotinoTest.Infrastructure.Error;
using NotinoTest.Infrastructure.Serializer;

namespace NotinoTest.BL.Feature.Convertor.Handler;

[Route(Routes.ConvertRoute)]
public class ConvertFileRequestHandler : EndpointBaseAsync
    .WithRequest<ConvertFileRequest>
    .WithResult<FileResult>
{
    private readonly IConvertorService _utils;
    private readonly ISerializer _storage;

    public ConvertFileRequestHandler(IConvertorService utils, ISerializer storage)
    {
        _utils = utils;
        _storage = storage;
    }

    [HttpPost("file"), DisableRequestSizeLimit]
    [ProducesResponseType(typeof(ResponseContent), StatusCodes.Status200OK)]
    [Consumes("multipart/form-data")]
    public override async Task<FileResult> HandleAsync([FromForm] ConvertFileRequest request,
        CancellationToken cancellationToken)
    {
        var length = request.Data.Length;
        if (length <= 0)
        {
            this.AddError("File doesnt have correct lenght");
            return default;
        }

        await using var fileStream = request.Data.OpenReadStream();
        using var streamReader = new StreamReader(fileStream);
        var resultString = await streamReader.ReadToEndAsync();
        
        return await _utils.Convert(resultString, request.ConvertTo).Match(async response =>
            {
                var memoryStream = new MemoryStream();
                await using var sw = new StreamWriter(memoryStream);
                await sw.WriteAsync(response);
                await sw.FlushAsync();
                return File(memoryStream.ToArray(), "application/octet-stream",
                    ConvertorHelper.FileNameStrategy[request.ConvertTo](
                        Path.GetFileNameWithoutExtension(request.Data.FileName)));
            },
            error =>
            {
                this.AddError(error);
                return default;
            });
    }
}