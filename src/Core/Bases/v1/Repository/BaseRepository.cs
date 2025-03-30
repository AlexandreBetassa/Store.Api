using Microsoft.EntityFrameworkCore;
using Store.Framework.Core.Bases.v1.Entities;
using Store.Framework.Core.Bases.v1.Interfaces;

namespace Store.Framework.Core.Bases.v1.Repository
{
    public class BaseRepository<T>(DbContext context) : IRepository<T>
        where T : BaseEntity
    {
        public DbContext Context { get; } = context;

        public async Task<T?> GetByIdAsync(int id) =>
            await Context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task CreateAsync(T entity) => await Context.Set<T>().AddAsync(entity);

        public async Task PatchStatusAsync(int id, bool status) =>
            await Context.Set<T>()
                .Where(x => x.Id.Equals(id))
                .ExecuteUpdateAsync(x => x.SetProperty(u => u.Status, status));

        public async Task SaveChangesAsync() =>
            await Context.SaveChangesAsync();
    }
}