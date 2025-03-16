using Autenticacao.Jwt.Application.Commands.v1.Users.CreateUser;
using Autenticacao.Jwt.Application.Commands.v1.Users.PatchStatusUser;
using Autenticacao.Jwt.Application.Constants.v1;
using Autenticacao.Jwt.Application.Queries.v1.GetUser;
using Autenticacao.Jwt.Filters.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao.Jwt.Controllers.v1
{
    [Route("api/v1/users")]
    [ApiController]
    [ServiceFilter(typeof(FilterHeader))]
    public class UserController : BaseController<UserController>
    {
        public UserController(IMediator mediator, ILoggerFactory loggerFactory)
            : base(mediator, loggerFactory)
        {
        }

        [HttpPatch("status")]
        [Authorize(Policy = AccessPolicies.Write)]
        public async Task<IActionResult> PatchAsync()
        {
            try
            {
                var userName = User?.Claims?.FirstOrDefault(c => c.Type == "name")?.Value;
                await Mediator.Send(new PatchStatusUserCommand(userName));

                return Created();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = AccessPolicies.Read)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var userName = User?.Claims?.FirstOrDefault(c => c.Type == "name")?.Value;

                var result = await Mediator.Send(new GetUserQuery(userName));

                return Ok(result);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand request)
        {
            try
            {
                await Mediator.Send(request);

                return Created();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}