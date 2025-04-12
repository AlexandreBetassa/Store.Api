using Fatec.Store.Framework.Core.Bases.v1.Repository;
using Fatec.Store.User.Domain.Entities.v1;
using Fatec.Store.User.Domain.Interfaces.v1.Repositories;
using Fatec.Store.User.Infrastructure.Data.Context;

namespace Fatec.Store.User.Infrastructure.Data.Repositories.v1
{
    public class LoginRepository(UserDbContext userContext)
        : BaseRepository<Login>(userContext), ILoginRepository
    {
    }
}
