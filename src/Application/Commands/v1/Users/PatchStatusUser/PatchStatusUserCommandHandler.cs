using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.Framework.Core.Bases.v1.CommandHandler;
using Store.User.Application.Extensions;
using Store.User.Domain.Interfaces.v1.Repositories;
using Store.User.Infrastructure.CrossCutting.Exceptions;
using System.Net;

namespace Store.User.Application.Commands.v1.Users.PatchStatusUser
{
    public class PatchStatusUserCommandHandler
        : BaseCommandHandler<PatchStatusUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public PatchStatusUserCommandHandler(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IUserRepository userRepository,
            IHttpContextAccessor contextAccessor)
            : base(loggerFactory.CreateLogger<PatchStatusUserCommandHandler>(), mapper, contextAccessor)
        {
            _userRepository = userRepository;
        }

        public override async Task<Unit> Handle(PatchStatusUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation("Inicio {handle}.{method}}", nameof(PatchStatusUserCommandHandler), nameof(Handle));

                var username = HttpContext.GetUserName();

                if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
                    throw new InvalidUserException(HttpStatusCode.BadRequest, "Dados do usuário inválido.");

                var user = await _userRepository.GetByUsernameAsync(username)
                    ?? throw new Exception("Usuário não localizado!!!");

                user.ChangeStatus();

                await _userRepository.PatchStatusAsync(user.Name, user.Status);

                Logger.LogInformation("Fim {handle}.{method}}", nameof(PatchStatusUserCommandHandler), nameof(Handle));

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{handle}.{method}", nameof(PatchStatusUserCommandHandler), nameof(Handle));

                throw new InternalErrorException();
            }
        }
    }
}