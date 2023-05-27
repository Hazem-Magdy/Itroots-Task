using Itroots_Task.Models;

namespace Itroots_Task.Services
{
    public interface IRoleRepository
    {
        Task<Role> getRoleAsync(string rolename);
        Task<List<Role>> getAllRolesAsync();
    }
}
