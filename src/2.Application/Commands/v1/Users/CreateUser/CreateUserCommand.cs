using Fatec.Store.User.Domain.Entities.v1;
using Fatec.Store.User.Domain.Enums.v1;
using Fatec.Store.User.Domain.Models.v1.Users;
using MediatR;

namespace Fatec.Store.User.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public Name Name { get; set; }

        public Login Login { get; set; }

        public DateTime Birthday { get; set; }

        public RolesUserEnum Role { get; set; }
    }
}
