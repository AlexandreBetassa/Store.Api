using FluentValidation;

namespace Fatec.Store.User.Application.Commands.v1.Auth.PutPassword
{
    public class PatchPasswordCommandValidator : AbstractValidator<PatchPasswordCommand>
    {
        public PatchPasswordCommandValidator()
        {
            RuleFor(x => x.RecoveryCode)
                .NotEmpty()
                .WithMessage("Código de recuperação não foi informada.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Nova senha não foi informada.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .WithMessage("Confirmação da nova senha não foi informada.");
        }
    }
}
