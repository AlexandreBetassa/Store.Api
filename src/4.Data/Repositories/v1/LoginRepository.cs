using Project.Framework.Core.v1.Bases.Repository;
using Store.Data.Context;
using Store.Domain.Entities.v1;
using Store.Domain.Interfaces.v1.Repositories;

namespace Store.Data.Repositories.v1
{
    public class LoginRepository(UserDbContext userContext) : BaseRepository<Login>(userContext), ILoginRepository
    {
    }
}
