using Project.Framework.Core.v1.Bases.Exceptions;
using System.Net;

namespace Project.CrossCutting.Exceptions
{
    public class InvalidUserException(string message = "Dados do usuário inválido.", HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : CustomException(statusCode, message)
    {
    }
}
