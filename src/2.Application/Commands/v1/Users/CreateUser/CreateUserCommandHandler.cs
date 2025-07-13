using Project.Framework.Core.v1.Bases.CommandHandler;

namespace Store.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommandHandler : BaseCommandHandler<CreateUserCommand, Unit>
    {
        private readonly IPasswordServices _passwordServices;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler
            (ILoggerFactory loggerFactory,
            IMapper mapper,
            IUserRepository userRepository,
            IPasswordServices passwordServices,
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
                var user = Mapper.Map<UserAccount>(request);

                user.Login.Password = _passwordServices.HashPassword(user.Login, request.Login.Password);

                await _userRepository.CreateAsync(user);

                await _userRepository.SaveChangesAsync();

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