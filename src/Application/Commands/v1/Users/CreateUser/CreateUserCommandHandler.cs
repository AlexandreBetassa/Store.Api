using Autenticacao.Jwt.Domain.Entities.v1;
using Autenticacao.Jwt.Domain.Interfaces.v1.Patterns;
using Autenticacao.Jwt.Domain.Interfaces.v1.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Autenticacao.Jwt.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommandHandler : BaseCommandHandler<CreateUserCommandHandler>, IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IPasswordServices<User> _passwordServices;
        public CreateUserCommandHandler
            (ILoggerFactory loggerFactory,
            IMapper mapper,
            IUnityOfWork unityOfWork,
            IPasswordServices<User> passwordServices)
            : base(loggerFactory, mapper, unityOfWork)
        {
            _passwordServices = passwordServices;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"Inicio metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                var user = Mapper.Map<User>(request);

                user.Password = _passwordServices.HashPassword(user, request.Password);

                await UnityOfWork.BeginTransactionAsync();
                await UnityOfWork.UserRepository.CreateAsync(user);
                await UnityOfWork.CommitAsync();

                Logger.LogInformation($"Fim metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Erro metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");
                await UnityOfWork.RollbackAsync();

                throw;
            }
        }
    }
}