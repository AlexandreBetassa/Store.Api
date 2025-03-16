using Autenticacao.Jwt.Domain.Enums.v1;
using MediatR;

namespace Autenticacao.Jwt.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RolesUserEnum Role { get; set; }
    }
}
