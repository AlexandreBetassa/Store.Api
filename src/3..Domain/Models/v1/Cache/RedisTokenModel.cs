namespace Store.Domain.Models.v1.Cache
{
    public class RedisTokenModel
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public RedisTokenModel(string token, string name, string email, string role)
        {
            Token = token;
            Name = name;
            Email = email;
            Role = role;
        }
    }
}
