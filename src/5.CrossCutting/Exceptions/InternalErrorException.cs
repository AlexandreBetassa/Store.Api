using Store.Framework.Core.v1.Bases.Exceptions;
using System.Net;

namespace Store.CrossCutting.Exceptions
{
    public class InternalErrorException(
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        string message = "Ocorreu um erro interno. Contate o administrador")
        : CustomException(statusCode, message)
    {
    }
}
