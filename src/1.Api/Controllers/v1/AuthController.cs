using Store.Application.Commands.v1.Auth.GenerateToken;
using Store.Application.Commands.v1.Auth.PutPassword;
using Store.Application.Commands.v1.Auth.SendEmailRecoveryPassword;
using Store.Framework.Core.v1.Bases.Controllers;

namespace Store.Api.Controllers.v1
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthController(IMediator mediator) : BaseController<AuthController>(mediator)
    {
        /// <summary>
        /// Generate Token v1.
        /// Responsável por gerar o token do usuário cadastrado.
        /// </summary>
        /// <param name="request">Dados de login.</param>
        /// <returns>O token válido.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GenerateTokenResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenCommand request) =>
             await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.Created);

        [HttpPost("recovery")]
        public async Task<IActionResult> RecoveryPassword([FromBody] RecoveryPasswordCommand request)
        {
            return await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.Created);
        }

        [HttpPatch("password")]
        public async Task<IActionResult> PatchPassword([FromBody] PatchPasswordCommand request)
        {
            return await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.NoContent);
        }
    }
}