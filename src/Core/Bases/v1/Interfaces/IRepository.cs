using Microsoft.EntityFrameworkCore.Query;
using Store.Framework.Core.Bases.v1.Entities;
using System.Linq.Expressions;

namespace Store.Framework.Core.Bases.v1.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task Updateasync(T entity);

        Task<T?> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task PatchAsync(int id, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression);

        Task SaveChangesAsync();
    }
}
