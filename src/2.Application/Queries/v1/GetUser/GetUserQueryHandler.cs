using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Store.Application.Queries.v1.GetUser
{
    public class GetUserQueryHandler(
        ILoggerFactory loggerFactory,
        IMapper mapper,
        IUserRepository userRepository,
        IHttpContextAccessor contextAccessor)
        : BaseCommandHandler<GetUserQuery, GetUserQueryResponse>(loggerFactory.CreateLogger<GetUserQueryHandler>(), mapper, contextAccessor)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public override async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var isValidId = int.TryParse(HttpContext.GetUserId(), out int id);

                if (!isValidId)
                    throw new InvalidUserException(HttpStatusCode.BadRequest, "Dados do usuário inválido.");

                Logger.LogInformation("Iniciando metodo {handler}.{method}", nameof(GetUserQueryHandler), nameof(Handle));

                var user = await _userRepository.GetByIdAsync(id);
                var response = Mapper.Map<GetUserQueryResponse>(user);

                Logger.LogInformation("Fim metodo {handler}.{method}", nameof(GetUserQueryHandler), nameof(Handle));

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{handler}.{method}", nameof(GetUserQueryHandler), nameof(Handle));

                throw new InternalErrorException();
            }
        }
    }
}