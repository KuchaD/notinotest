using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor;
using NotinoTest.api.Convertor.Response;
using NotinoTest.BL.Feature.Convertor.Request;
using NotinoTest.Infrastructure;

namespace NotinoTest.BL.Feature.Convertor.Handler;

[Route(Routes.ConvertRoute)]
public class ConvertRequestHandler : EndpointBaseAsync
    .WithRequest<ConvertRequest>
    .WithActionResult<ResponseContent>
{
    private readonly IConvertorService _convertorService;

    public ConvertRequestHandler(IConvertorService convertorService)
    {
        _convertorService = convertorService;
    }

    [HttpPost("text")]
    [ProducesResponseType(typeof(ResponseContent), StatusCodes.Status200OK)]
    public override async Task<ActionResult<ResponseContent>> HandleAsync(ConvertRequest request,
        CancellationToken cancellationToken)
    {
        return _convertorService.Convert(request.Content, request.To).Match(
            response => new ResponseContent(response),
            error =>
            {
                this.AddError(error);
                return default;
            });
    }
}