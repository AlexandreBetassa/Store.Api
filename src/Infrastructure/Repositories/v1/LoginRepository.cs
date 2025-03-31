using Store.Framework.Core.Bases.v1.Repository;
using Store.User.Domain.Entities.v1;
using Store.User.Domain.Interfaces.v1.Repositories;

namespace Store.User.Infrastructure.Data.Repositories.v1
{
    public class LoginRepository(UserDbContext userContext) 
        : BaseRepository<Login>(userContext), ILoginRepository
    {
    }
}
