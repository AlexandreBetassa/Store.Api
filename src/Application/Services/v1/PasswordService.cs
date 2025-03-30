using Microsoft.AspNetCore.Identity;
using Store.User.Domain.Entities.v1;
using Store.User.Domain.Interfaces.v1.Services;

namespace Store.User.Application.Services.v1
{
    public class PasswordService<T> : IPasswordServices<T> where T : Login
    {
        private readonly PasswordHasher<T> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<T>();
        }

        public string HashPassword(T login, string password) => _passwordHasher.HashPassword(login, password);

        public bool VerifyPassword(T login, string hashedPassword, string password) =>
             _passwordHasher.VerifyHashedPassword(login, hashedPassword, password).Equals(PasswordVerificationResult.Success);
    }
}
