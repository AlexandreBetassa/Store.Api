using Project.Framework.Core.v1.Bases.Exceptions;
using System.Net;

namespace Project.CrossCutting.Exceptions
{
    public class InvalidUserException(HttpStatusCode statusCode, string message) : CustomException(statusCode, message)
    {
    }
}
