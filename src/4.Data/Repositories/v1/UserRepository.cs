using Microsoft.EntityFrameworkCore;
using Store.Data.Context;

namespace Store.Data.Repositories.v1
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