using System.Net;

namespace Store.CrossCutting.Exceptions
{
    public class NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        : CustomException(statusCode, message)
    {
    }
}
