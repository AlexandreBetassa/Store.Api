namespace Store.User.Domain.Interfaces.v1.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.v1.User> GetByEmailAsync(string email);

        Task<Entities.v1.User> GetByIdAsync(int id);

        Task CreateAsync(Entities.v1.User user);

        Task PatchStatusAsync(int id, bool status);

        Task SaveChangesAsync();
    }
}