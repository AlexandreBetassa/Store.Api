using Autenticacao.Jwt.Domain.Entities.v1;

namespace Autenticacao.Jwt.Domain.Interfaces.v1.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string username);
        Task CreateAsync(User user);
        Task PatchStatusAsync(string username, bool status);
    }
}