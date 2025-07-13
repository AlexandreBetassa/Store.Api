namespace Store.Domain.Models.v1.Cache
{
    public class RecoveryPasswordCache(string email, string recoveryCode, string userId)
    {
        public string LoginId { get; set; } = userId;

        public string Email { get; } = email;

        public string RecoveryCode { get; } = recoveryCode;
    }
}
