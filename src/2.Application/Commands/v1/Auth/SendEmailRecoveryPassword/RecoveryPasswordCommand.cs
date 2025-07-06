using MediatR;

namespace Store.Application.Commands.v1.Auth.SendEmailRecoveryPassword
{
    public class RecoveryPasswordCommand : IRequest<Unit>
    {
        /// <summary>
        /// O email do usuário cadastrado.
        /// </summary>
        public string Email { get; set; }
    }
}
