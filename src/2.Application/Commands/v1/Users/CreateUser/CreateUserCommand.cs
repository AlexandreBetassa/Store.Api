using MediatR;

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
