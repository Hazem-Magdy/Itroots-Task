using Itroots_Task.Data;
using Itroots_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Itroots_Task.Services
{
    public class UserRepository : IUserRepository
    {
        AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            User existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser != null)
            {
                _db.Users.Remove(existingUser);

                await _db.SaveChangesAsync();
            }

        }

        public async Task<User> existingUserAsync(string username, string password)
        {
            return await _db.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _db.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            User existingUser = await _db.Users.Include(u=>u.Role).FirstOrDefaultAsync(x => x.Id == id);

            return existingUser;
        }
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            User existingUser = await _db.Users.FirstOrDefaultAsync(x => x.UserName == username);

            return existingUser;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User existingEmail = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

            return existingEmail;
        }


        public async Task UpdateUserAsync(User user)
        {
            User existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser != null)
            {
  
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.FullName = user.FullName;
                existingUser.Phone = user.Phone;
                existingUser.RoleId = user.RoleId;

                await _db.SaveChangesAsync();
            }
        }

        
    }
}
