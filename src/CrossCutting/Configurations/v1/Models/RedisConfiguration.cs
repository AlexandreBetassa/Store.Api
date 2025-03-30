namespace Store.User.Infrastructure.CrossCutting.Configurations.v1.Models
{
    public class RedisConfiguration
    {
        public string Server { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}