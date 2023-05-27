using Itroots_Task.Models;

namespace Itroots_Task.Services
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);

        Task<User> existingUserAsync (string username,string password);
        Task<User> GetUserByUserNameAsync(string username);

        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task<User> AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
