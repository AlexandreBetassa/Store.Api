using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao.Jwt.Controllers.v1
{
    public class BaseController<T> : ControllerBase
    {
        public IMediator Mediator { get; set; }
        public ILogger Logger { get; set; }

        public BaseController(IMediator mediator, ILoggerFactory loggerFactory)
        {
            Mediator = mediator;
            Logger = loggerFactory.CreateLogger<T>();
        }
    }
}
