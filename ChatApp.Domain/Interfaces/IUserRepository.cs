using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByNameAsync(string userName);
    }
}