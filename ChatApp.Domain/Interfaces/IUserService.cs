using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByNameAsync(string userId);
        Task RegisterUserAsync(User user);
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}