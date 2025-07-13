using Project.Framework.Core.v1.Bases.Exceptions;
using System.Net;

namespace Project.CrossCutting.Exceptions
{
    public class NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        : CustomException(statusCode, message)
    {
    }
}
