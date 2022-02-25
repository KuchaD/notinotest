using System.Net;

namespace NotinoTest.Infrastructure.Error;

public class ErrorType
{
    public string Name { get; set; }
    public int ErrorCode { get; set; } = (int)HttpStatusCode.BadRequest;
    public string ErrorMessage { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new();

    public ErrorType() {}

    public ErrorType(HttpStatusCode code)
    {
        ErrorCode = (int)code;
    }

    public ErrorType(string message)
    {
        ErrorMessage = message;
    }

    public static implicit operator ErrorType(string message) => new ErrorType { ErrorMessage = message };

}