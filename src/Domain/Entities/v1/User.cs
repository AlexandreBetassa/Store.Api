using Store.User.Domain.Enums.v1;
using Store.User.Domain.Models.v1.Users;

namespace Store.User.Domain.Entities.v1
{
    public class User
    {
        public int Id { get; set; }

        public Name Name { get; set; }

        public Login Login { get; set; }

        public DateTime Birthday { get; set; }

        public bool Status { get; set; }

        public RolesUserEnum Role { get; set; }

        public void ChangeStatus() => Status = !Status;
    }
}