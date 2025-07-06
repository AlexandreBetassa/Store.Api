using Store.Data.Context;
using Store.Domain.Entities.v1;
using Store.Domain.Interfaces.v1.Repositories;
using Store.Framework.Core.v1.Bases.Repository;

namespace Store.Data.Repositories.v1
{
    public class LoginRepository(UserDbContext userContext) : BaseRepository<Login>(userContext), ILoginRepository
    {
    }
}
