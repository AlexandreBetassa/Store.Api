using Store.Domain.Enums.v1;
using Store.Domain.Models.v1.Users;
using Store.Framework.Core.Bases.v1.Entities;

namespace Store.Domain.Entities.v1
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