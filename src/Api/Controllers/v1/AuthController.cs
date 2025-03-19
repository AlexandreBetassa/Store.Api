using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Framework.Core.Bases.v1.Controllers;
using Store.User.Application.Commands.v1.Users.GenerateToken;
using System.Net;

namespace Store.User.Api.Controllers.v1
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthController(IMediator mediator) : BaseController<AuthController>(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenCommand request) =>
             await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.Created);
    }
}