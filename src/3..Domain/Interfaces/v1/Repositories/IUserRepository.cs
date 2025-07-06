using Fatec.Store.Framework.Core.Bases.v1.Interfaces;

namespace Fatec.Store.User.Domain.Interfaces.v1.Repositories
{
    public interface IUserRepository : IRepository<Entities.v1.User>
    {
        Task<Entities.v1.User> GetByEmailOrUsernameAsync(string email);
    }
}