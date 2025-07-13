using Microsoft.Extensions.Options;
using Project.CrossCutting.Configurations.v1;
using Project.CrossCutting.Exceptions;
using Project.Framework.Core.v1.Bases.CommandHandler;
using Store.Application.Enums.v1;
using Store.CrossCutting.Configurations.v1.Models;
using Store.Domain.Models.v1.Cache;

namespace Store.Application.Commands.v1.Auth.SendEmailRecoveryPassword
{
    public class RecoveryPasswordCommandHandler : BaseCommandHandler<RecoveryPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordServices _passwordService;
        private readonly IEmailService _emailService;
        private readonly IEnumerable<EmailTemplates> _emailTemplates;

        private readonly string _userEmail;

        public RecoveryPasswordCommandHandler(
        IOptions<Appsettings> appsettingsConfigurations,
        ILoggerFactory loggerFactory,
        IMapper mapper,
        IHttpContextAccessor httpContext,
        IUserRepository userRepository,
        IPasswordServices passwordServices,
        IEmailService emailService) : base(loggerFactory.CreateLogger<RecoveryPasswordCommandHandler>(), mapper, httpContext)
        {
            _userRepository = userRepository;
            _passwordService = passwordServices;
            _emailService = emailService;
            _userEmail = httpContext.GetUserEmail();
            _emailTemplates = appsettingsConfigurations.Value.EmailConfiguration.EmailTemplates;
        }

        public override async Task<Unit> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmailOrUsernameAsync(request.Email)
                    ?? throw new NotFoundException("Usuário não localizado!!!");

                var recoveryCode = GenerateRecoveryCode();
                await _passwordService.PersistCacheRecoveryPassword(new RecoveryPasswordCache(request.Email, recoveryCode, user.Login.Id.ToString()));

                var emailTemplate = _emailTemplates.FirstOrDefault(x => x.Type.Equals(nameof(TypeEmailEnum.RecoveryPassword)));

                await _emailService.SendEmailAsync(
                    toEmail: request.Email,
                    subject: emailTemplate.Subject,
                    body: string.Format(emailTemplate.Body, recoveryCode));

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Erro metodo {nameof(RecoveryPasswordCommandHandler)}.{nameof(Handle)}");

                throw;
            }
        }

        private static string GenerateRecoveryCode() =>
            Random.Shared.Next(0, 999999).ToString("000000");
    }
}