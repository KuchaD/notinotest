using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor;
using NotinoTest.api.Convertor.Response;
using NotinoTest.Infrastructure;
using NotinoTest.Infrastructure.Error;

namespace NotinoTest.BL.Feature.Convertor.Handler;

[Route(Routes.ConvertRoute)]
public class ConvertRequestHandler : EndpointBaseAsync
    .WithRequest<ConvertRequest>
    .WithActionResult<ResponseContent>
{
    private readonly IConvertorService _utils;

    public ConvertRequestHandler(ILogger<ConvertRequestHandler> logger, IConvertorService utils)
    {
        _utils = utils;
    }

    [HttpPost("text")]
    [ProducesResponseType(typeof(ResponseContent), StatusCodes.Status200OK)]
    public override async Task<ActionResult<ResponseContent>> HandleAsync(ConvertRequest request,
        CancellationToken cancellationToken)
    {
        return _utils.Convert(request.Content, request.To).Match(
            response => new ResponseContent(response),
            error =>
            {
                this.AddError(error);
                return default;
            });
    }
}