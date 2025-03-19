using System.Text.Json.Serialization;

namespace Store.User.Domain.Entities.v1
{
    public class Login
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}