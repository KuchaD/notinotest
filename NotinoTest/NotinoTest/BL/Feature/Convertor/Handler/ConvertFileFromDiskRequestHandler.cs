using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor;
using NotinoTest.api.Convertor.Handler;
using NotinoTest.Infrastructure;
using NotinoTest.Infrastructure.Error;

namespace NotinoTest.BL.Feature.Convertor.Handler;

[Route(Routes.ConvertRoute)]
public class ConvertFileFromDiskRequestHandler : EndpointBaseAsync
    .WithRequest<ConvertFileFromDiskRequest>
    .WithActionResult
{
    private readonly IConvertorService _utils;
    private readonly IStorage _storage;

    public ConvertFileFromDiskRequestHandler(IConvertorService utils, IStorage storage)
    {
        _utils = utils;
        _storage = storage;
    }
    
    [HttpPost("file-disk"), DisableRequestSizeLimit]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult> HandleAsync(ConvertFileFromDiskRequest request,
        CancellationToken cancellationToken)
    {
        if (!_storage.Exist(request.SourcePath))
        {
            this.AddError($"File {request.SourcePath} doesnt exists.");
            return BadRequest();
        }
        
        var resultString = await _storage.ReadTextAsync(request.SourcePath, cancellationToken);
        return await _utils.Convert(resultString, request.ConvertTo).Match<Task<ActionResult>>( 
            async (response) =>
            {
                await _storage.WriteTextAsync(request.TargerPath, response, cancellationToken);
                return Ok();
            }, 
            async error =>
            {
                this.AddError(error);
                return BadRequest();
            });
    }
}