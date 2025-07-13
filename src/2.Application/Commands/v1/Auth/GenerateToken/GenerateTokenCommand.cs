namespace Store.Application.Commands.v1.Auth.GenerateToken
{
    public class GenerateTokenCommand : IRequest<GenerateTokenResponse>
    {
        /// <summary>
        /// Email ou nome de usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha.
        /// </summary>
        public string Password { get; set; }
    }
}