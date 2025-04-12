using Fatec.Store.Framework.Core.Bases.v1.Entities;
using Fatec.Store.User.Domain.Enums.v1;
using Fatec.Store.User.Domain.Models.v1.Users;

namespace Fatec.Store.User.Domain.Entities.v1
{
    public class User : BaseEntity
    {
        public Name Name { get; set; }

        public Login Login { get; set; }

        public DateTime Birthday { get; set; }

        public RolesUserEnum Role { get; set; }

        public void ChangeStatus() => Status = !Status;
    }
}