using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.Framework.Core.Bases.v1.CommandHandler;
using Store.User.Application.Shared.Extensions;
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

                _ = int.TryParse(HttpContext.GetUserId(), out int id);

                if (id is 0)
                    throw new InvalidUserException(HttpStatusCode.BadRequest, "Dados do usuário inválido.");

                var user = await _userRepository.GetByIdAsync(id)
                    ?? throw new Exception("Usuário não localizado!!!");

                user.ChangeStatus();

                await _userRepository.PatchStatusAsync(id, user.Status);
                await _userRepository.SaveChangesAsync();

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