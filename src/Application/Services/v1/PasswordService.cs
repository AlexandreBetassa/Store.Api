using Fatec.Store.User.Domain.Entities.v1;
using Fatec.Store.User.Domain.Interfaces.v1.Services;
using Fatec.Store.User.Domain.Models.v1.Cache;
using Microsoft.AspNetCore.Identity;

namespace Fatec.Store.User.Application.Services.v1
{
    public class PasswordService : IPasswordServices
    {
        private readonly PasswordHasher<Login> _passwordHasher;
        private readonly IRedisService _redisService;

        private static readonly string _cacheKey = "recovery";

        public PasswordService(IRedisService redisService)
        {
            _passwordHasher = new PasswordHasher<Login>();
            _redisService = redisService;
        }

        public string HashPassword(Login login, string password) =>
            _passwordHasher.HashPassword(login, password);

        public bool VerifyPassword(Login login, string hashedPassword, string password) =>
             _passwordHasher.VerifyHashedPassword(login, hashedPassword, password).Equals(PasswordVerificationResult.Success);

        public async Task PersistCacheRecoveryPassword(RecoveryPasswordCache cache) =>
            await _redisService.CreateAsync(string.Format($"{_cacheKey}_{cache.RecoveryCode}_{cache.Email}"), cache);

        public async Task<string> GetRecoveryPasswordCacheAsync(string recoveryCode, string email) =>
            await _redisService.GetKey(string.Format($"{_cacheKey}_{recoveryCode}_{email}"));

        public async Task DeleteCacheRecoveryPassword(RecoveryPasswordCache cache) =>
            await _redisService.DeleteCache(string.Format($"{_cacheKey}_{cache.RecoveryCode}_{cache.Email}"));
    }
}