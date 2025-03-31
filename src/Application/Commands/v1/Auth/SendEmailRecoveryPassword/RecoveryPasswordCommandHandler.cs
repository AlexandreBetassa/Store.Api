using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.Framework.Core.Bases.v1.CommandHandler;
using Store.User.Domain.Interfaces.v1.Repositories;
using Store.User.Domain.Interfaces.v1.Services;
using Store.User.Domain.Models.v1.Cache;
using Store.User.Infrastructure.CrossCutting.Exceptions;

namespace Store.User.Application.Commands.v1.Auth.SendEmailRecoveryPassword
{
    public class RecoveryPasswordCommandHandler : BaseCommandHandler<RecoveryPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordServices _passwordService;

        public RecoveryPasswordCommandHandler(
        ILoggerFactory loggerFactory,
        IMapper mapper,
        IHttpContextAccessor httpContext,
        IUserRepository userRepository,
        IPasswordServices passwordServices) : base(loggerFactory.CreateLogger<RecoveryPasswordCommandHandler>(), mapper, httpContext)
        {
            _userRepository = userRepository;
            _passwordService = passwordServices;
        }


        public override async Task<Unit> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmailOrUsernameAsync(request.Email)
                    ?? throw new NotFoundException("Usuário não localizado!!!");

                await _passwordService.PersistCacheRecoveryPassword(
                    new RecoveryPasswordCache(request.Email, GenerateRecoveryCode(), user.Login.Id));

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