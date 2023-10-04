using System.Net;

namespace API.Utilities.Handler;

public class ResponseServerErrorHandler
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public string? Error { get; set; }
    
    public ResponseServerErrorHandler(string message)
    {
        Code = StatusCodes.Status500InternalServerError;
        Status = HttpStatusCode.InternalServerError.ToString();
        Message = message;
    }
    
    public ResponseServerErrorHandler(string message, string error)
    {
        Code = StatusCodes.Status500InternalServerError;
        Status = HttpStatusCode.InternalServerError.ToString();
        Message = message;
        Error = error;
    }
}