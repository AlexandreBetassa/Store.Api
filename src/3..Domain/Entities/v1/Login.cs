using Project.Framework.Core.v1.Bases.Entities;

namespace Store.Domain.Entities.v1
{
    public class Login : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}