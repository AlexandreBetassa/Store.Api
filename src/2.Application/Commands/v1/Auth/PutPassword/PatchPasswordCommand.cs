namespace Store.Application.Commands.v1.Auth.PutPassword
{
    public class PatchPasswordCommand : IRequest<Unit>
    {
        /// <summary>
        /// O email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// O código de recuperação.
        /// </summary>
        public string RecoveryCode { get; set; }

        /// <summary>
        /// A nova senha.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirmação da nova senha
        /// </summary>
        public string ConfirmNewPassword { get; set; }
    }
}
