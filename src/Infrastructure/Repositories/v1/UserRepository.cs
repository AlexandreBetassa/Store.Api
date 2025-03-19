using Microsoft.EntityFrameworkCore;
using Store.User.Domain.Interfaces.v1.Repositories;

namespace Store.User.Infrastructure.Data.Repositories.v1
{
    public class UserRepository(UserContext userContext) : IUserRepository
    {
        private readonly UserContext _userContext = userContext;

        public async Task CreateAsync(Domain.Entities.v1.User user) =>
            await _userContext.Set<Domain.Entities.v1.User>().AddAsync(user);

        public async Task<Domain.Entities.v1.User?> GetByEmailAsync(string email) =>
             await _userContext.Set<Domain.Entities.v1.User>()
                .AsNoTracking()
                .Include(x => x.Login)
                .FirstOrDefaultAsync(x => x.Login.Email.Equals(email));


        public async Task<Domain.Entities.v1.User?> GetByIdAsync(int id) =>
            await _userContext.Set<Domain.Entities.v1.User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task PatchStatusAsync(int id, bool status) =>
            await _userContext.Set<Domain.Entities.v1.User>()
                .Where(x => x.Id.Equals(id))
                .ExecuteUpdateAsync(x => x.SetProperty(u => u.Status, status));

        public async Task SaveChangesAsync() => await _userContext.SaveChangesAsync();
    }
}