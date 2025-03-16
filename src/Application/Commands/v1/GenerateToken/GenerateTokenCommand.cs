using MediatR;

namespace Autenticacao.Jwt.Application.Commands.v1.GenerateToken
{
    public class GenerateTokenCommand : IRequest<GenerateTokenResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}