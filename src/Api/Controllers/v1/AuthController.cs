using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Framework.Core.Bases.v1.Controllers;
using Store.User.Application.Commands.v1.Auth.GenerateToken;
using Store.User.Infrastructure.CrossCutting.Exceptions;
using System.Net;

namespace Store.User.Api.Controllers.v1
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

    }
}