using Store.User.Domain.Entities.v1;

namespace Store.User.Domain.Interfaces.v1.Services
{
    public interface IPasswordServices<T> where T : Login
    {
        string HashPassword(T login, string password);
        bool VerifyPassword(T login, string hashedPassword, string password);
    }
}