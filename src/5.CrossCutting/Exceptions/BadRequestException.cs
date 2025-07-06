using System.Net;

namespace Store.CrossCutting.Exceptions
{
    public class BadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : CustomException(statusCode, message)
    {
    }
}
