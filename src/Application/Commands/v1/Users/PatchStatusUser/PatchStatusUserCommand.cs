using MediatR;

namespace Autenticacao.Jwt.Application.Commands.v1.Users.PatchStatusUser
{
    public class PatchStatusUserCommand(string username) : IRequest<Unit>
    {
        public string Username { get; set; } = username;
    }
}