using Autenticacao.Jwt.Application.Commands.v1.GenerateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Autenticacao.Jwt.Controllers.v1
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthController : BaseController<AuthController>
    {
        public AuthController(IMediator mediator, ILoggerFactory loggerFactory)
            : base(mediator, loggerFactory)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenCommand request)
        {
            try
            {
                var token = await Mediator.Send(request);

                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(JsonSerializer.Serialize(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}