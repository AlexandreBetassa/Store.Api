using System.Net;

namespace Store.CrossCutting.Exceptions
{
    public class InvalidUserException(HttpStatusCode statusCode, string message) : CustomException(statusCode, message)
    {
    }
}
