namespace Autenticacao.Jwt.Domain.Interfaces.v1.Services
{
    public interface IPasswordServices<T> where T : class
    {
        string HashPassword(T user, string password);
        bool VerifyPassword(T user, string hashedPassword, string password);
    }
}