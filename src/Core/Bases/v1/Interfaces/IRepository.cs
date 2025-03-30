using Store.Framework.Core.Bases.v1.Entities;

namespace Store.Framework.Core.Bases.v1.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task PatchStatusAsync(int id, bool status);

        Task SaveChangesAsync();
    }
}
