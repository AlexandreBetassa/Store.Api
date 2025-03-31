using Store.Framework.Core.Bases.v1.Exceptions;
using System.Net;

namespace Store.User.Infrastructure.CrossCutting.Exceptions
{
    public class BadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : CustomException(statusCode, message)
    {
    }
}
