using System.Net;

namespace API.Utilities.Handler;

public class ResponseOKHandler<TEntity>
{
    public ResponseOKHandler(TEntity? data)
    {
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = "Success to Retrieve Data";
        Data = data;
    }

    public ResponseOKHandler(string message)
    {
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = message;
    }

    public ResponseOKHandler(string message, TEntity data)
    {
        Code = StatusCodes.Status200OK;
        Status = HttpStatusCode.OK.ToString();
        Message = message;
        Data = data;
    }

    public ResponseOKHandler()
    {
        
    }

    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public TEntity? Data { get; set; }
}