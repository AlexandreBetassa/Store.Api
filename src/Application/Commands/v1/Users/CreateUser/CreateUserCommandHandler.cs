using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.Framework.Core.Bases.v1.CommandHandler;
using Store.User.Domain.Interfaces.v1.Repositories;
using Store.User.Domain.Interfaces.v1.Services;
using Store.User.Infrastructure.CrossCutting.Exceptions;
using UserAccount = Store.User.Domain.Entities.v1.User;

namespace Store.User.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommandHandler : BaseCommandHandler<CreateUserCommand, Unit>
    {
        private readonly IPasswordServices<UserAccount> _passwordServices;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler
            (ILoggerFactory loggerFactory,
            IMapper mapper,
            IUserRepository userRepository,
            IPasswordServices<UserAccount> passwordServices,
            IHttpContextAccessor contextAccessor)
            : base(loggerFactory.CreateLogger<CreateUserCommandHandler>(), mapper, contextAccessor)
        {
            _passwordServices = passwordServices;
            _userRepository = userRepository;
        }

        public override async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"Inicio metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                var user = Mapper.Map<UserAccount>(request);

                user.Password = _passwordServices.HashPassword(user, request.Password);

                await _userRepository.CreateAsync(user);

                Logger.LogInformation($"Fim metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Erro metodo {nameof(CreateUserCommandHandler)}.{nameof(Handle)}");

                throw new InternalErrorException();
            }
        }
    }
}