using Project.Framework.Core.v1.Bases.Exceptions;
using System.Net;

namespace Store.CrossCutting.Exceptions
{
    public class BadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : CustomException(statusCode, message)
    {
    }
}
