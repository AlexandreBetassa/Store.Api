namespace Store.CrossCutting.Configurations.v1.Models
{
    public class JwtConfiguration
    {
        public string SecretJwtKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string[] WriteRoles { get; set; }
        public string[] ReadRoles { get; set; }
    }
}