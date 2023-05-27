using Itroots_Task.Data;
using Itroots_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Itroots_Task.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _db;

        public RoleRepository(AppDbContext db) {
            _db = db;
        }
        public async Task<Role> getRoleAsync( string rolename)
        {
            Role role =  await _db.Roles.FirstOrDefaultAsync(r => r.Name == rolename);
            
            return role;

        }
        public async Task<List<Role>> getAllRolesAsync()
        {
            List<Role> roleList = await _db.Roles.ToListAsync();

            return roleList;

        }
    }
}
