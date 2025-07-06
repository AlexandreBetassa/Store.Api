using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Filters.v1;
using System.Net;

namespace Store.Api.Controllers.v1
{
    [Route("api/v1/users")]
    [ApiController]
    [ServiceFilter(typeof(FilterHeader))]
    public class UserController(IMediator mediator) : BaseController<UserController>(mediator)
    {
        [HttpPatch("status")]
        [Authorize(Policy = nameof(AccessPoliciesEnum.Write))]
        public async Task<IActionResult> PatchAsync() =>
            await ExecuteAsync(async () => await Mediator.Send(new PatchStatusUserCommand()), HttpStatusCode.NoContent);

        [HttpGet]
        [Authorize(Policy = nameof(AccessPoliciesEnum.Write))]
        public async Task<IActionResult> GetAsync() =>
            await ExecuteAsync(async () => await Mediator.Send(new GetUserQuery()), HttpStatusCode.OK);

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand request) =>
            await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.Created);
    }
}