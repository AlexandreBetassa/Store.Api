namespace Store.User.Domain.Interfaces.v1.Services
{
    public interface IRedisService
    {
        Task CreateAsync(string key, object data, int expirationInMinutes = 15);
        Task<string> GetKey(string key);
    }
}
