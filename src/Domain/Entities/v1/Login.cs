using Store.Framework.Core.Bases.v1.Entities;
using System.Text.Json.Serialization;

namespace Store.User.Domain.Entities.v1
{
    public class Login : BaseEntity
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}