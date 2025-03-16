using Autenticacao.Jwt.Domain.Interfaces.v1.Services;
using Microsoft.AspNetCore.Identity;

namespace Autenticacao.Jwt.Application.Services.v1
{
    public class PasswordService<T> : IPasswordServices<T> where T : class
    {
        private readonly PasswordHasher<T> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<T>();
        }

        public string HashPassword(T user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(T user, string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
