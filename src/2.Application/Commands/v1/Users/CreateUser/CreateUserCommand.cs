using Store.Domain.Entities.v1;
using Store.Domain.Enums.v1;
using Store.Domain.Models.v1.Users;

namespace Store.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// O nome do usuário.
        /// </summary>
        public Name Name { get; set; }

        /// <summary>
        /// O login do usuário.
        /// </summary>
        public Login Login { get; set; }

        /// <summary>
        /// A data de aniversário.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// O tipo de usuário.
        /// </summary>
        public RolesUserEnum Role { get; set; }
    }
}
