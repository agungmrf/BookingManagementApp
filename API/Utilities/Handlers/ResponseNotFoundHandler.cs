using System.Net;

namespace API.Utilities.Handler;

public class ResponseNotFoundHandler
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public string? Error { get; set; }
    
    public ResponseNotFoundHandler(string message)
    {
        Code = StatusCodes.Status404NotFound;
        Status = HttpStatusCode.NotFound.ToString();
        Message = message;
    }
    
    public ResponseNotFoundHandler(string message, string error)
    {
        Code = StatusCodes.Status404NotFound;
        Status = HttpStatusCode.NotFound.ToString();
        Message = message;
        Error = error;
    }
}