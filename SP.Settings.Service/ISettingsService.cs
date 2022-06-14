using SP.Settings.Models;

namespace SP.Settings.Service
{
    public interface ISettingsService
    {
        Role? GetRole(string role);
        void CreateRole(Role role);
        IEnumerable<Role>? GetRoles();
    }
}