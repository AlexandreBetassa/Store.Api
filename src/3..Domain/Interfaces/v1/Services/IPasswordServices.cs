﻿using Store.Domain.Entities.v1;
using Store.Domain.Models.v1.Cache;

namespace Store.Domain.Interfaces.v1.Services
{
    public interface IPasswordServices
    {
        string HashPassword(Login login, string password);

        bool VerifyPassword(Login login, string hashedPassword, string password);

        Task PersistCacheRecoveryPassword(RecoveryPasswordCache cache);

        Task DeleteCacheRecoveryPassword(RecoveryPasswordCache cache);

        Task<string> GetRecoveryPasswordCacheAsync(string recoveryCode, string email);
    }
}