using Project.Framework.Core.v1.Bases.Entities;
using System.Text.Json.Serialization;

namespace Store.Domain.Entities.v1
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