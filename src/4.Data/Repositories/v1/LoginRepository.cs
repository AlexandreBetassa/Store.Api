using Store.Data.Context;

namespace Store.Data.Repositories.v1
{
    public class LoginRepository(UserDbContext userContext)
        : BaseRepository<Login>(userContext), ILoginRepository
    {
    }
}
