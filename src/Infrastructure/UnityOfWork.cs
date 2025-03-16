using Autenticacao.Jwt.Domain.Interfaces.v1.Patterns;
using Autenticacao.Jwt.Domain.Interfaces.v1.Repositories;
using System.Data;

namespace Autenticacao.Jwt.Infrastructure
{
    public class UnitOfWork : IUnityOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;

        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(IDbConnection dbConnection, IUserRepository userRepository)
        {
            _dbConnection = dbConnection;
            UserRepository = userRepository;
        }

        public async Task BeginTransactionAsync()
        {
            if (_dbTransaction is null)
            {
                _dbConnection.Open();
                _dbTransaction = _dbConnection.BeginTransaction();
                await Task.CompletedTask;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                _dbTransaction?.Commit();
                _dbConnection.Close();
            }
            finally
            {
                Dispose();
            }
            await Task.CompletedTask;
        }

        public async Task RollbackAsync()
        {
            try
            {
                _dbTransaction?.Rollback();
            }
            finally
            {
                Dispose();
            }
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _dbTransaction?.Dispose();
            _dbConnection?.Dispose();
        }
    }
}