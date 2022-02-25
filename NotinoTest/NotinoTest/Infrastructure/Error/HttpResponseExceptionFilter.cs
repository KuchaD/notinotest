using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NotinoTest.Infrastructure.Error;

public class HttpResponseExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var logger = (ILogger<HttpResponseExceptionFilter>)context.HttpContext.RequestServices.GetService(typeof(ILogger<HttpResponseExceptionFilter>))!;
        logger.LogError("UnhandleExpetion ErrorCode={@errorcode} Exeption={@Exeption}", (int)HttpStatusCode.InternalServerError, context.Exception);
        context.Result = CreateExceptionResponse(context.Exception);
        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }

    private static ObjectResult CreateExceptionResponse(Exception exception)
    {
        var errorResponse = new ErrorType()
        {
            Name = exception.GetType().Name,
            ErrorMessage = exception.Message,
            ErrorCode = (int)HttpStatusCode.InternalServerError
        };

        return new ObjectResult(errorResponse) { StatusCode = errorResponse.ErrorCode };
    }
}