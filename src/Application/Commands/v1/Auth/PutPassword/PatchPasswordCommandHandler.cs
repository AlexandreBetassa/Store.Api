using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Framework.Core.Bases.v1.CommandHandler;
using Store.User.Domain.Interfaces.v1.Repositories;
using Store.User.Domain.Interfaces.v1.Services;
using Store.User.Domain.Models.v1.Cache;
using Store.User.Infrastructure.CrossCutting.Exceptions;

namespace Store.User.Application.Commands.v1.Auth.PutPassword
{
    public class PutPasswordCommandHandler : BaseCommandHandler<PatchPasswordCommand, Unit>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordServices _passwordServices;

        public PutPasswordCommandHandler(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            ILoginRepository loginRepository,
            IPasswordServices passwordServices)
            : base(loggerFactory.CreateLogger<PutPasswordCommandHandler>(), mapper, httpContext)
        {
            _loginRepository = loginRepository;
            _passwordServices = passwordServices;
        }

        public override async Task<Unit> Handle(PatchPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.NewPassword.Equals(request.ConfirmNewPassword))
                    throw new BadRequestException("As senhas não conferem.");

                var cache = await _passwordServices.GetRecoveryPasswordCacheAsync(request.RecoveryCode, request.Email);

                if (string.IsNullOrEmpty(cache))
                    throw new BadRequestException("Código de verificação inválido.");

                var recoveryCache = JsonConvert.DeserializeObject<RecoveryPasswordCache>(cache);
                var login = await _loginRepository.GetByIdAsync(recoveryCache.LoginId);

                var newPassword = _passwordServices.HashPassword(login, request.NewPassword);
                await _loginRepository.PatchAsync(login.Id, x => x.SetProperty(x => x.Password, newPassword));
                await _passwordServices.DeleteCacheRecoveryPassword(recoveryCache);

                await _loginRepository.SaveChangesAsync();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{handle}.{method}", nameof(PutPasswordCommandHandler), nameof(Handle));

                throw;
            }
        }
    }
}
