﻿using Fatec.Store.Framework.Core.Bases.v1.Repository;
using Fatec.Store.User.Domain.Interfaces.v1.Repositories;
using Fatec.Store.User.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Fatec.Store.User.Infrastructure.Data.Repositories.v1
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