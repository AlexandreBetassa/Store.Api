using Fatec.Store.Framework.Core.Bases.v1.Exceptions;
using System.Net;

namespace Fatec.Store.User.Infrastructure.CrossCutting.Exceptions
{
    public class BadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : CustomException(statusCode, message)
    {
    }
}
