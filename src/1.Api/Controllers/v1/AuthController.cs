using Microsoft.AspNetCore.Authorization;
using Store.Application.Commands.v1.Auth.GenerateToken;
using Store.Application.Commands.v1.Auth.PutPassword;
using Store.Application.Commands.v1.Auth.SendEmailRecoveryPassword;

namespace Store.Api.Controllers.v1
{
    /// <summary>
    /// The authentication controller.
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthController(IMediator mediator) : BaseController<AuthController>(mediator)
    {
        /// <summary>
        /// Generate token v1.
        /// </summary>
        /// <param name="request">Os dados de login.</param>
        /// <remarks>
        /// <b>Sobre:</b>
        /// <p>Responsável por gerar um token.</p>
        /// <b>Requisitos:</b>
        /// <p>Usuário estar cadastrado no sistema.</p>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(GenerateTokenResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GenerateTokenAsync([FromBody] GenerateTokenCommand request) =>
             await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.Created);

        /// <summary>
        /// Recovery password v1.
        /// </summary>
        /// <param name="request">Os dados para recuperação de senha.</param>
        /// <remarks>
        /// <b>Sobre:</b>
        /// <p>Responsável por recuperação de senha.</p>
        /// <b>Requisitos:</b>
        /// <p>Usuário estar cadastrado no sistema.</p>
        /// </remarks>
        [HttpPost("recovery")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> RecoveryPasswordAsync([FromBody] RecoveryPasswordCommand request)
        {
            return await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.Created);
        }

        /// <summary>
        /// Patch password v1.
        /// </summary>
        /// <param name="request">Os dados para alteração de senha.</param>
        /// <remarks>
        /// <b>Sobre:</b>
        /// <p>Responsável por alterar a senha.</p>
        /// <b>Requisitos:</b>
        /// <p>Usuário estar cadastrado no sistema.</p>
        /// </remarks>
        [HttpPatch("password")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> PatchPasswordAsync([FromBody] PatchPasswordCommand request)
        {
            return await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.NoContent);
        }
    }
}