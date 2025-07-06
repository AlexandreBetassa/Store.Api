using Fatec.Store.Framework.Core.Bases.v1.Controllers;
using Fatec.Store.User.Application.Commands.v1.Auth.GenerateToken;
using Fatec.Store.User.Application.Commands.v1.Auth.PutPassword;
using Fatec.Store.User.Application.Commands.v1.Auth.SendEmailRecoveryPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fatec.Store.User.Api.Controllers.v1
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