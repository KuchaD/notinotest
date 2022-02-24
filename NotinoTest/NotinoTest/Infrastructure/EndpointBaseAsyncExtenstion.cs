using System.Net;
using Ardalis.ApiEndpoints;
using NotinoTest.Infrastructure.Error;

namespace NotinoTest.Infrastructure;

public static class EndpointBaseAsyncExtenstion
{
    public static void AddError(this EndpointBase endpoint, ErrorType error)
    {
        endpoint.HttpContext.Items.Add("Error", error);
    }
    
    public static void AddError(this EndpointBase endpoint, ErrorType error, HttpStatusCode code)
    {
        error.ErrorCode = (int)code ;
        endpoint.HttpContext.Items.Add("Error", error);
    }

    public static void AddValidateError(this EndpointBase endpoint, (object key, string error) validateError)
    {
        endpoint.HttpContext.Items.TryGetValue("Error", out var item);
        ErrorType error;
        if (item is not null)
        {
            if (item is not ErrorType)
            {
                throw new InvalidCastException("Error must be ErrorType");
            }

            error = (ErrorType)item;
        }
        else
        {
            error = new ErrorType();
        }

        if (error.Errors.ContainsKey(nameof(validateError.key)))
            error.Errors[nameof(validateError.key)] += $"; {validateError.error}";
        else
            error.Errors.Add(nameof(validateError.key), validateError.error);
    }
}