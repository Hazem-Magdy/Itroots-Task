using Itroots_Task.Data;
using Itroots_Task.Models;

namespace Itroots_Task.Services
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _db;

        public UserRoleRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddUserRoleAsync(Models.UserRole userRole)
        {
           await _db.UserRoles.AddAsync(userRole);

           await _db.SaveChangesAsync();
        }
    }
}
