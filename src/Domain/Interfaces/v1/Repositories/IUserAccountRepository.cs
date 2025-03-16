namespace Store.User.Domain.Interfaces.v1.Repositories
{
    public interface IUserAccountRepository
    {
        Task<Entities.v1.User> GetByUsernameAsync(string username);
        Task<Entities.v1.User> GetByEmailAsync(string username);
        Task CreateAsync(Entities.v1.User user);
        Task PatchStatusAsync(string username, bool status);
    }
}