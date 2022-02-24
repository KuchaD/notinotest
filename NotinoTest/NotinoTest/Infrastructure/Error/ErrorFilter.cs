using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NotinoTest.Infrastructure.Serializer;
using OneOf;

namespace NotinoTest.Infrastructure.Error;


public class ErrorFilter : ActionFilterAttribute
{
    private ISerializer _serializer;
        
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.HttpContext.Items.TryGetValue("Error", out var errorValue))
        {
            if (errorValue is not ErrorType) 
                return;
            
            _serializer = (ISerializer)context.HttpContext.RequestServices.GetService(typeof(ISerializer))!;
            ErrorType error = (ErrorType)errorValue;
            context.Result = new ObjectResult(error); //new ContentResult{ Content = _serializer.Serialize(error), ContentType = "application/json" };
            context.HttpContext.Response.StatusCode = error.ErrorCode;
        }
    }
}