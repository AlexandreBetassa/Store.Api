using System.Net;

namespace Store.CrossCutting.Exceptions
{
    public class UnauthorizedException(HttpStatusCode statusCode, string message) : CustomException(statusCode, message)
    {
    }
}
