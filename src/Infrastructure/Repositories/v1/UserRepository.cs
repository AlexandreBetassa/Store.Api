using Microsoft.EntityFrameworkCore;
using Store.Framework.Core.Bases.v1.Repository;
using Store.User.Domain.Interfaces.v1.Repositories;

namespace Store.User.Infrastructure.Data.Repositories.v1
{
    public class UserRepository(UserDbContext userContext) : BaseRepository<Domain.Entities.v1.User>(userContext), IUserRepository
    {
        public async Task<Domain.Entities.v1.User?> GetByEmailOrUsernameAsync(string email) =>
            await Context.Set<Domain.Entities.v1.User>()
            .AsNoTracking()
            .Include(user => user.Login)
            .FirstOrDefaultAsync(user => user.Login.Email.Equals(email) || user.Login.UserName.Equals(email));
    }
}