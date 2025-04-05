using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.Framework.Core.Bases.v1.CommandHandler;
using Store.User.Application.Enums.v1;
using Store.User.Application.Shared.Extensions;
using Store.User.Domain.Interfaces.v1.Repositories;
using Store.User.Domain.Interfaces.v1.Services;
using Store.User.Domain.Models.v1.Cache;
using Store.User.Infrastructure.CrossCutting.Configurations.v1;
using Store.User.Infrastructure.CrossCutting.Configurations.v1.Models;
using Store.User.Infrastructure.CrossCutting.Exceptions;

namespace Store.User.Application.Commands.v1.Auth.SendEmailRecoveryPassword
{
    public class RecoveryPasswordCommandHandler : BaseCommandHandler<RecoveryPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordServices _passwordService;
        private readonly IEmailService _emailService;
        private readonly IEnumerable<EmailTemplates> _emailTemplates;

        private readonly string _userEmail;

        public RecoveryPasswordCommandHandler(
        AppsettingsConfigurations appsettingsConfigurations,
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
            _emailTemplates = appsettingsConfigurations.EmailConfiguration.EmailTemplates;
        }


        public override async Task<Unit> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmailOrUsernameAsync(request.Email)
                    ?? throw new NotFoundException("Usuário não localizado!!!");

                var recoveryCode = GenerateRecoveryCode();
                await _passwordService.PersistCacheRecoveryPassword(new RecoveryPasswordCache(request.Email, recoveryCode, user.Login.Id));

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