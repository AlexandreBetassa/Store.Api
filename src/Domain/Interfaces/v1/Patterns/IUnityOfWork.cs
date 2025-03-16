using Autenticacao.Jwt.Domain.Interfaces.v1.Repositories;

namespace Autenticacao.Jwt.Domain.Interfaces.v1.Patterns
{
    public interface IUnityOfWork : IDisposable
    {
        IUserRepository UserRepository { get; set; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
