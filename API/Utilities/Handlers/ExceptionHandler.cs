namespace API.Utilities.Handler;

public class ExceptionHandler : Exception
{
    public ExceptionHandler(string message) : base(message)
    {
    }
}