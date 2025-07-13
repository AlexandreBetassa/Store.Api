using Microsoft.AspNetCore.Authorization;
using Project.Framework.Core.v1.Enums;
using Store.Application.Commands.v1.Users.CreateUser;
using Store.Application.Commands.v1.Users.PatchStatusUser;
using Store.Application.Queries.v1.GetUser;

namespace Store.Api.Controllers.v1
{
    /// <summary>
    /// Responsável por gerenciar dados dos usuários.
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/v1/users")]
    [ApiController]
    public class UserController(IMediator mediator) : BaseController<UserController>(mediator)
    {
        /// <summary>
        /// Patch status v1.
        /// </summary>
        /// <remarks>
        /// <b>Sobre:</b>
        /// <p>Responsável por alterar o status de um usuário.</p>
        /// <b>Requisitos:</b>
        /// <p>Usuário estar autenticado.</p>
        /// </remarks>
        [HttpPatch("status")]
        [Authorize(Policy = nameof(AccessPoliciesEnum.Write))]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchStatusAsync() =>
            await ExecuteAsync(async () => await Mediator.Send(new PatchStatusUserCommand()), HttpStatusCode.NoContent);

        /// <summary>
        /// Get user by id v1.
        /// </summary>
        /// <remarks>
        /// <b>Sobre:</b>
        /// <p>Responsável por buscar os dados de um usuário por id.</p>
        /// <b>Requisitos:</b>
        /// <p>Usuário estar autenticado.</p>
        /// </remarks>
        [HttpGet]
        [Authorize(Policy = nameof(AccessPoliciesEnum.Write))]
        [ProducesResponseType(typeof(GetUserQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserByIdAsync() =>
            await ExecuteAsync(async () => await Mediator.Send(new GetUserQuery()), HttpStatusCode.OK);

        /// <summary>
        /// Create user v1.
        /// </summary>
        /// <remarks>
        /// <b>Sobre:</b>
        /// <p>Responsável por criar um usuário.</p>
        /// <b>Requisitos:</b>
        /// </remarks>
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand request) =>
            await ExecuteAsync(async () => await Mediator.Send(request), HttpStatusCode.Created);
    }
}