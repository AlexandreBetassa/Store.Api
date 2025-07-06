using Store.Domain.Entities.v1;
using Store.Domain.Enums.v1;
using Store.Domain.Models.v1.Users;

namespace Store.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public Name Name { get; set; }

        public Login Login { get; set; }

        public DateTime Birthday { get; set; }

        public RolesUserEnum Role { get; set; }
    }
}
