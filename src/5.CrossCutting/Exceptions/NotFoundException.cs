using Store.Framework.Core.v1.Bases.Exceptions;
using System.Net;

namespace Store.CrossCutting.Exceptions
{
    public class NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        : CustomException(statusCode, message)
    {
    }
}
