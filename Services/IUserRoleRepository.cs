namespace Itroots_Task.Services
{
    public interface IUserRoleRepository
    {
        Task AddUserRoleAsync(Models.UserRole userRole);
    }
}
