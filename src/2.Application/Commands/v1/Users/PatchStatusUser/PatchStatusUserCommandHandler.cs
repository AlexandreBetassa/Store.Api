using Project.CrossCutting.Exceptions;
using Project.Framework.Core.v1.Bases.CommandHandler;

namespace Store.Application.Commands.v1.Users.PatchStatusUser
{
    public class PatchStatusUserCommandHandler : BaseCommandHandler<PatchStatusUserCommand, Unit>
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
                var userId = HttpContext.GetUserId();

                if (string.IsNullOrEmpty(userId))
                    throw new InvalidUserException();

                var user = await _userRepository.GetByIdAsync(userId)
                    ?? throw new NotFoundException("Usuário não localizado!!!");

                user.ChangeStatus();


                await _userRepository.PatchAsync(userId, x => x.SetProperty(x => x.Status, user.Status));
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