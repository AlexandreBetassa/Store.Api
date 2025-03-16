using Store.Framework.Core.Bases.v1.Exceptions;
using System.Net;

namespace Store.User.Infrastructure.CrossCutting.Exceptions
{
    public class InternalErrorException(
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        string message = "Ocorreu um erro interno. Contate o administrador")
        : CustomException(statusCode, message)
    {
    }
}
