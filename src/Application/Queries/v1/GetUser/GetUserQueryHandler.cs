using Autenticacao.Jwt.Application.Commands.v1;
using Autenticacao.Jwt.Domain.Interfaces.v1.Patterns;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Autenticacao.Jwt.Application.Queries.v1.GetUser
{
    public class GetuserQueryHandler : BaseCommandHandler<GetuserQueryHandler>, IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        public GetuserQueryHandler(ILoggerFactory loggerFactory, IMapper mapper, IUnityOfWork unityOfWork)
            : base(loggerFactory, mapper, unityOfWork)
        {
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"Iniciando metodo {nameof(GetuserQueryHandler)}.{Handle}");

                var user = await UnityOfWork.UserRepository.GetByUsernameAsync(request.Name);
                var response = Mapper.Map<GetUserQueryResponse>(user);

                Logger.LogInformation($"Fim metodo {nameof(GetuserQueryHandler)}.{Handle}");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Metodo {nameof(GetuserQueryHandler)}.{nameof(Handle)}");

                throw;
            }
        }
    }
}