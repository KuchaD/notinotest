using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using NotinoTest.api.Convertor;
using NotinoTest.api.Convertor.Response;
using NotinoTest.BL.Feature.Convertor.Request;
using NotinoTest.Infrastructure;
using NotinoTest.Infrastructure.Email;

namespace NotinoTest.BL.Feature.Convertor.Handler;

[Route(Routes.ConvertRoute)]
public class ConvertFileAndSendToEmailRequestHandle : EndpointBaseAsync
    .WithRequest<ConvertFileAndSendToEmailRequest>
    .WithActionResult
{
    private readonly IConvertorService _convertorService;
    private readonly IEmailClient _emailClient;

    public ConvertFileAndSendToEmailRequestHandle(IConvertorService convertorService, IEmailClient emailClient)
    {
        _convertorService = convertorService;
        _emailClient = emailClient;
    }

    [HttpPost("email/file"), DisableRequestSizeLimit]
    [ProducesResponseType(typeof(ResponseContent), StatusCodes.Status200OK)]
    [Consumes("multipart/form-data")]
    public override async Task<ActionResult> HandleAsync([FromForm] ConvertFileAndSendToEmailRequest request,
        CancellationToken cancellationToken)
    {
        var length = request.File.Length;
        if (length <= 0)
        {
            this.AddError("File doesnt have correct lenght");
            return default;
        }

        await using var fileStream = request.File.OpenReadStream();
        using var streamReader = new StreamReader(fileStream);
        var resultString = await streamReader.ReadToEndAsync();

        return await _convertorService.Convert(resultString, request.ConvertTo).Match<Task<ActionResult>>(
            async response =>
            {
                await SendEmailAsync(request, response);
                return Ok();
            },
            async error =>
            {
                this.AddError(error);
                return BadRequest();
            });
    }

    private async Task SendEmailAsync(ConvertFileAndSendToEmailRequest request, string response)
    {
        var memoryStream = new MemoryStream();
        await using var sw = new StreamWriter(memoryStream);
        await sw.WriteAsync(response);
        await sw.FlushAsync();

        var email = new EmailBuilder()
            .FromDefault()
            .To(request.Email)
            .SimpleBody("Your file is in attachment")
            .AddAttachment(ConvertorHelper.FileNameStrategy[request.ConvertTo]("resultFile"), memoryStream)
            .Build();

        var result = _emailClient.TrySend(email);
        if (result is not null)
            this.AddError(result);
    }
}