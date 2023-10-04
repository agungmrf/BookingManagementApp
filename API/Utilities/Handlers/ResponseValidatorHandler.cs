using System.Net;

namespace API.Utilities.Handler;

public class ResponseValidatorHandler
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public object Error { get; set; }

    public ResponseValidatorHandler(object error)
    {
        Code = StatusCodes.Status400BadRequest;
        Status = HttpStatusCode.BadRequest.ToString();
        Message = "Validation Error";
        Error = error;
    }
}