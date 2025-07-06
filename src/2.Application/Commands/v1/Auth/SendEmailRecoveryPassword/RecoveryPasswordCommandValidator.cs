namespace Store.Application.Commands.v1.Auth.SendEmailRecoveryPassword
{
    public class RecoveryPasswordCommandValidator : AbstractValidator<RecoveryPasswordCommand>
    {
        public RecoveryPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email não doi informado.");
        }
    }
}