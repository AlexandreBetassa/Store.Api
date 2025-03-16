using Autenticacao.Jwt.Domain.Interfaces.v1.Patterns;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Autenticacao.Jwt.Application.Commands.v1.Users.PatchStatusUser
{
    public class PatchStatusUserCommandHandler
        : BaseCommandHandler<PatchStatusUserCommandHandler>, IRequestHandler<PatchStatusUserCommand, Unit>
    {
        public PatchStatusUserCommandHandler(ILoggerFactory loggerFactory, IMapper mapper, IUnityOfWork unityOfWork)
            : base(loggerFactory, mapper, unityOfWork)
        {
        }

        public async Task<Unit> Handle(PatchStatusUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"Start method {nameof(PatchStatusUserCommandHandler)}.{nameof(Handle)} || Username={request.Username}");

                var user = await UnityOfWork.UserRepository.GetByUsernameAsync(request.Username)
                    ?? throw new Exception("Usuário não localizado!!!");

                user.ChangeStatus();

                await UnityOfWork.BeginTransactionAsync();
                await UnityOfWork.UserRepository.PatchStatusAsync(user.Name, user.Status);
                await UnityOfWork.CommitAsync();

                Logger.LogInformation($"Finish method {nameof(PatchStatusUserCommandHandler)}.{nameof(Handle)} || Username={request.Username}");

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(PatchStatusUserCommandHandler)}.{nameof(Handle)} || Username={request.Username}");

                throw;
            }
        }
    }
}