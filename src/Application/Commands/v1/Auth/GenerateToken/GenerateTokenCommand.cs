using MediatR;

namespace Store.User.Application.Commands.v1.Auth.GenerateToken
{
    public class GenerateTokenCommand : IRequest<GenerateTokenResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}