using Fatec.Store.Framework.Core.Bases.v1.Controllers;
using Fatec.Store.Framework.Core.Enums;
using Fatec.Store.User.Api.Filters.v1;
using Fatec.Store.User.Application.Commands.v1.Users.CreateUser;
using Fatec.Store.User.Application.Commands.v1.Users.PatchStatusUser;
using Fatec.Store.User.Application.Queries.v1.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fatec.Store.User.Api.Controllers.v1
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