using Autenticacao.Jwt.Domain.Interfaces.v1.Patterns;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Autenticacao.Jwt.Application.Commands.v1
{
    public abstract class BaseCommandHandler<T>
    {
        public IMapper Mapper { get; set; }
        public ILogger Logger { get; set; }
        public IUnityOfWork UnityOfWork { get; set; }

        protected BaseCommandHandler(ILoggerFactory loggerFactory, IMapper mapper, IUnityOfWork unityOfWork)
        {
            Logger = loggerFactory.CreateLogger<T>();
            Mapper = mapper;
            UnityOfWork = unityOfWork;
        }
    }
}