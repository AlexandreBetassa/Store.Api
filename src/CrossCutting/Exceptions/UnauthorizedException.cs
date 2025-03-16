using Store.Framework.Core.Bases.v1.Exceptions;
using System.Net;

namespace Store.User.Infrastructure.CrossCutting.Exceptions
{
    public class UnauthorizedException(HttpStatusCode statusCode, string message) : CustomException(statusCode, message)
    {
    }
}
