namespace Store.Application.Commands.v1.Auth.PutPassword
{
    public class PatchPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }

        public string RecoveryCode { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}
